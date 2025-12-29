using Do_an.Models;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Do_an.Firebase
{
    public class FirebaseDatabaseService
    {
        private readonly FirebaseClient _firebaseClient;
        private static readonly HttpClient _httpClient = new HttpClient();

        public FirebaseDatabaseService()
        {
            _firebaseClient = new FirebaseClient(FirebaseConfig.DatabaseUrl);
        }

        // --- 1. USER & PROFILE ---
        public async Task<User> GetUserAsync(string uid)
        {
            try { return await _firebaseClient.Child("Users").Child(uid).OnceSingleAsync<User>(); }
            catch { return null; }
        }

        public async Task<UserProfile> GetUserProfileAsync(string uid)
        {
            try { var p = await _firebaseClient.Child("Users").Child(uid).Child("Info").OnceSingleAsync<UserProfile>(); return p ?? new UserProfile(); }
            catch { return new UserProfile(); }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try { var users = await _firebaseClient.Child("Users").OnceAsync<User>(); return users.Select(item => item.Object).ToList(); }
            catch { return new List<User>(); }
        }

        public async Task UpdateUserProfileAsync(string uid, string name, string bio)
        {
            await _firebaseClient.Child("Users").Child(uid).Child("Username").PutAsync($"\"{name}\"");
            await _firebaseClient.Child("Users").Child(uid).Child("Info").Child("Bio").PutAsync($"\"{bio}\"");
        }

        public async Task UpdateUserStatsAsync(string uid, UserProfile info)
        {
            await _firebaseClient.Child("Users").Child(uid).Child("Info").PutAsync(info);
        }

        public async Task UpdateUserAvatarAsync(string uid, string base64Image)
        {
            try
            {
                var data = new { AvatarBase64 = base64Image };
                string json = JsonConvert.SerializeObject(data);
                var method = new HttpMethod("PATCH");
                var request = new HttpRequestMessage(method, $"{FirebaseConfig.DatabaseUrl}/Users/{uid}.json") { Content = new StringContent(json, Encoding.UTF8, "application/json") };
                await _httpClient.SendAsync(request);
            }
            catch (Exception ex) { throw new Exception("Lỗi upload avatar: " + ex.Message); }
        }

        public async Task UpdateUserLocalIPAsync(string uid, string ip)
        {
            try
            {
                var data = new { LocalIP = ip };
                string json = JsonConvert.SerializeObject(data);
                var method = new HttpMethod("PATCH");
                var request = new HttpRequestMessage(method, $"{FirebaseConfig.DatabaseUrl}/Users/{uid}.json") { Content = new StringContent(json, Encoding.UTF8, "application/json") };
                await _httpClient.SendAsync(request);
            }
            catch { }
        }

        // =========================================================
        // [MỚI] TÍNH TOÁN XẾP HẠNG (RANK) & TOP 3
        // Hàm này xử lý logic Hạng tuần, Hạng cao nhất, Lần lọt top
        // =========================================================
        public async Task CalculateAndSaveRankAsync(string myUid)
        {
            try
            {
                // 1. Lấy tất cả user về để so sánh
                var allUsers = await GetAllUsersAsync();

                // 2. Sắp xếp: Ai nhiều Giờ học nhất đứng đầu, nếu bằng nhau thì so Level
                var sortedList = allUsers
                    .OrderByDescending(u => u.Info.TotalHours)
                    .ThenByDescending(u => u.Info.Level)
                    .ToList();

                // 3. Tìm vị trí của mình trong danh sách
                int myIndex = sortedList.FindIndex(u => u.Uid == myUid);

                if (myIndex != -1)
                {
                    var myUser = sortedList[myIndex];

                    int currentRank = myIndex + 1;          // Hạng mới tính được (Bắt đầu từ 1)
                    int oldRank = myUser.Info.WeeklyRank;   // Hạng cũ đang lưu trong DB

                    // --- A. Cập nhật Hạng tuần này ---
                    myUser.Info.WeeklyRank = currentRank;

                    // --- B. Cập nhật Hạng cao nhất (All Time) ---
                    // Nếu chưa có hạng (0) hoặc hạng mới cao hơn (nhỏ hơn) hạng cũ -> Cập nhật
                    if (myUser.Info.HighestRank == 0 || currentRank < myUser.Info.HighestRank)
                    {
                        myUser.Info.HighestRank = currentRank;
                    }

                    // --- C. Cập nhật Số lần lọt Top (Top 3) ---
                    if (currentRank <= 3) // Nếu đang đứng trong Top 3
                    {
                        // Trường hợp 1: Chưa từng được tính (đang là 0) -> Gán ngay bằng 1
                        if (myUser.Info.TotalTopReach == 0)
                        {
                            myUser.Info.TotalTopReach = 1;
                        }
                        // Trường hợp 2: Trước đó đang ở ngoài Top (hạng > 3 hoặc chưa có hạng) 
                        // mà giờ nhảy vào Top 3 -> Thì mới tính là thêm 1 lần mới
                        else if (oldRank > 3 || oldRank == 0)
                        {
                            myUser.Info.TotalTopReach++;
                        }
                    }

                    // 4. Lưu lại thông tin mới lên Firebase
                    await _firebaseClient
                        .Child("Users")
                        .Child(myUid)
                        .Child("Info")
                        .PutAsync(myUser.Info);
                }
            }
            catch { }
        }

        // --- TÍNH ĐIỂM CHUỖI NGÀY (STREAK) ---
        public async Task CheckAndUpdateStreakAsync(string uid)
        {
            try
            {
                var user = await GetUserAsync(uid);
                if (user == null) return;

                DateTime today = DateTime.Now.Date;
                DateTime lastLogin = DateTime.MinValue;

                if (!string.IsNullOrEmpty(user.Info.LastActive))
                {
                    DateTime.TryParse(user.Info.LastActive, out lastLogin);
                }

                bool needUpdate = false;

                if (lastLogin.Date == today)
                {
                    return; // Đã tính hôm nay rồi
                }
                else if (lastLogin.Date == today.AddDays(-1))
                {
                    user.Info.CurrentStreak++; // Liên tiếp
                    needUpdate = true;
                }
                else
                {
                    user.Info.CurrentStreak = 1; // Mất chuỗi
                    needUpdate = true;
                }

                if (user.Info.CurrentStreak > user.Info.BestStreak)
                {
                    user.Info.BestStreak = user.Info.CurrentStreak;
                    needUpdate = true;
                }

                if (needUpdate)
                {
                    user.Info.LastActive = DateTime.Now.ToString("o");
                    await _firebaseClient.Child("Users").Child(uid).Child("Info").PutAsync(user.Info);
                }
            }
            catch { }
        }

        // --- 7. TÍNH ĐIỂM & LÊN CẤP (LEVEL UP) ---
        public async Task AddXpAndLevelUpAsync(string uid, int xpEarned, double hoursEarned)
        {
            try
            {
                var user = await GetUserAsync(uid);
                if (user == null) return;

                user.Info.XP += xpEarned;
                user.Info.TotalHours += hoursEarned;

                while (user.Info.XP >= user.Info.XPToNextLevel)
                {
                    user.Info.XP -= user.Info.XPToNextLevel;
                    user.Info.Level++;
                    user.Info.XPToNextLevel += 50;
                }

                await _firebaseClient.Child("Users").Child(uid).Child("Info").PutAsync(user.Info);
            }
            catch (Exception ex) { throw new Exception("Lỗi đồng bộ điểm: " + ex.Message); }
        }

        // --- CÁC HÀM KHÁC GIỮ NGUYÊN ---
        public async Task SendFriendRequestAsync(string myUid, string keyword)
        {
            var all = await GetAllUsersAsync();
            var target = all.FirstOrDefault(u => (u.Email != null && u.Email.Equals(keyword, StringComparison.OrdinalIgnoreCase)) || (u.Username != null && u.Username.Equals(keyword, StringComparison.OrdinalIgnoreCase)));
            if (target == null) throw new Exception("Không tìm thấy người dùng.");
            if (target.Uid == myUid) throw new Exception("Không thể kết bạn với chính mình.");
            await _firebaseClient.Child("FriendRequests").Child(target.Uid).Child(myUid).PutAsync("true");
        }

        public async Task<List<User>> GetPendingRequestsAsync(string myUid)
        {
            var reqs = await _firebaseClient.Child("FriendRequests").Child(myUid).OnceAsync<string>();
            var list = new List<User>();
            foreach (var r in reqs) { var u = await GetUserAsync(r.Key); if (u != null) list.Add(u); }
            return list;
        }

        public async Task AcceptFriendRequestAsync(string myUid, string senderUid)
        {
            await _firebaseClient.Child("Users").Child(myUid).Child("Friends").Child(senderUid).PutAsync(true);
            await _firebaseClient.Child("Users").Child(senderUid).Child("Friends").Child(myUid).PutAsync(true);
            await _firebaseClient.Child("FriendRequests").Child(myUid).Child(senderUid).DeleteAsync();
        }

        public async Task<List<User>> GetFriendsAsync(string myUid)
        {
            var friends = await _firebaseClient.Child("Users").Child(myUid).Child("Friends").OnceAsync<bool>();
            var list = new List<User>();
            foreach (var f in friends) { var u = await GetUserAsync(f.Key); if (u != null) list.Add(u); }
            return list;
        }

        public class GroupInfo { public string GroupId { get; set; } public string Name { get; set; } public string OwnerUid { get; set; } public Dictionary<string, bool> Members { get; set; } = new Dictionary<string, bool>(); }

        public async Task CreateGroupAsync(string name, string ownerUid, List<string> members)
        {
            var gid = Guid.NewGuid().ToString();
            var g = new GroupInfo { GroupId = gid, Name = name, OwnerUid = ownerUid };
            g.Members.Add(ownerUid, true); foreach (var m in members) g.Members.Add(m, true);
            await _firebaseClient.Child("Groups").Child(gid).PutAsync(g);
            foreach (var uid in g.Members.Keys) await _firebaseClient.Child("Users").Child(uid).Child("Groups").Child(gid).PutAsync(true);
        }

        public async Task<List<GroupInfo>> GetUserGroupsAsync(string myUid)
        {
            var list = new List<GroupInfo>();
            try
            {
                var myGroups = await _firebaseClient.Child("Users").Child(myUid).Child("Groups").OnceAsync<bool>();
                foreach (var item in myGroups) { var g = await _firebaseClient.Child("Groups").Child(item.Key).OnceSingleAsync<GroupInfo>(); if (g != null) { g.GroupId = item.Key; list.Add(g); } }
            }
            catch { }
            return list;
        }

        public async Task SendGroupMessageAsync(string gid, string uid, string name, object msg)
        {
            await _firebaseClient.Child("GroupMessages").Child(gid).PostAsync(msg);
        }

        public async Task LeaveGroupAsync(string gid, string uid)
        {
            await _firebaseClient.Child("Users").Child(uid).Child("Groups").Child(gid).DeleteAsync();
        }

        public async Task SendMessageAsync(string f, string t, object msg)
        {
            await _firebaseClient.Child("Messages").Child(t).PostAsync(msg);
            await _firebaseClient.Child("Messages").Child(f).PostAsync(msg);
        }

        public async Task DeleteConversationAsync(string myUid, string partnerUid)
        {
            try
            {
                var items = await _firebaseClient.Child("Messages").Child(myUid).OnceAsync<dynamic>();
                foreach (var item in items)
                {
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(item.Object);
                    if (json.Contains($"\"peerUid\":\"{partnerUid}\"") || json.Contains($"\"peerEmail\":\"{partnerUid}\""))
                        await _firebaseClient.Child("Messages").Child(myUid).Child(item.Key).DeleteAsync();
                }
            }
            catch (Exception ex) { throw new Exception("Lỗi xóa tin: " + ex.Message); }
        }

        public async Task SendCallRequestAsync(string receiverUid, CallInfo callData)
        {
            await _firebaseClient.Child("CallRequests").Child(receiverUid).PutAsync(callData);
        }

        public async Task<CallInfo> CheckIncomingCallAsync(string myUid)
        {
            try
            {
                string url = $"{FirebaseConfig.DatabaseUrl}/CallRequests/{myUid}.json?t={DateTime.Now.Ticks}";
                string json = await _httpClient.GetStringAsync(url);
                if (string.IsNullOrEmpty(json) || json == "null") return null;
                return JsonConvert.DeserializeObject<CallInfo>(json);
            }
            catch { return null; }
        }

        public async Task UpdateCallStatusAsync(string targetUid, string status)
        {
            await _firebaseClient.Child("CallRequests").Child(targetUid).Child("Status").PutAsync($"\"{status}\"");
        }

        public async Task EndCallAsync(string uid)
        {
            await _firebaseClient.Child("CallRequests").Child(uid).DeleteAsync();
        }

        public async Task<List<string>> GetGroupMemberUidsAsync(string groupId)
        {
            var list = new List<string>();
            try
            {
                var members = await _firebaseClient.Child("Groups").Child(groupId).Child("Members").OnceAsync<bool>();
                foreach (var m in members) list.Add(m.Key);
            }
            catch { }
            return list;
        }

        public async Task UpdateUserActivity(string uid, bool isOnline)
        {
            try
            {
                await _firebaseClient.Child("Users").Child(uid).Child("IsOnline").PutAsync(isOnline);
                await _firebaseClient.Child("Users").Child(uid).Child("LastActive").PutAsync($"\"{DateTime.UtcNow:o}\"");
            }
            catch { }
        }

        public async Task AddTaskAsync(string uid, DateTime date, TaskInfo task)
        {
            try
            {
                string dateStr = date.ToString("yyyy-MM-dd");
                var result = await _firebaseClient.Child("Schedules").Child(uid).Child(dateStr).PostAsync(task);
                task.Id = result.Key;
                await _firebaseClient.Child("Schedules").Child(uid).Child(dateStr).Child(task.Id).PutAsync(task);
            }
            catch (Exception ex) { throw new Exception("Lỗi lưu lịch: " + ex.Message); }
        }

        public async Task<List<TaskInfo>> GetTasksByDateAsync(string uid, DateTime date)
        {
            try
            {
                string dateStr = date.ToString("yyyy-MM-dd");
                var items = await _firebaseClient.Child("Schedules").Child(uid).Child(dateStr).OnceAsync<TaskInfo>();
                return items.Select(i => i.Object).ToList();
            }
            catch { return new List<TaskInfo>(); }
        }

        public async Task UpdateTaskStatusAsync(string uid, DateTime date, string taskId, bool isDone)
        {
            try
            {
                string dateStr = date.ToString("yyyy-MM-dd");
                await _firebaseClient.Child("Schedules").Child(uid).Child(dateStr).Child(taskId).Child("IsDone").PutAsync(isDone);
            }
            catch { }
        }
    }
}