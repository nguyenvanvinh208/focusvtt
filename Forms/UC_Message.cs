using Do_an.Firebase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Do_an.Forms
{
    public partial class UC_Message : UserControl
    {
        public event Action OnMenuClicked;
        private readonly FirebaseDatabaseService _dbService = new FirebaseDatabaseService();
        private static readonly HttpClient Http = new HttpClient();

        private ContextMenuStrip _emojiMenu;
        private ContextMenuStrip _friendMenu;

        // --- THAY ĐỔI MÀU BONG BÓNG CHAT (THEME VINTAGE) ---
        // Tin nhắn của mình: Màu gỗ tối/da bò
        private readonly Color clrMineMsg = Color.FromArgb(101, 67, 33);
        // Tin nhắn người khác: Màu giấy cũ/Parchment
        private readonly Color clrOtherMsg = Color.FromArgb(218, 200, 162);
        // ---------------------------------------------------

        private readonly Dictionary<string, List<ChatMessage>> _conversations = new Dictionary<string, List<ChatMessage>>();
        private readonly Dictionary<string, HashSet<string>> _seenMessageIds = new Dictionary<string, HashSet<string>>();
        private readonly Dictionary<string, string> _idToName = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _idToUid = new Dictionary<string, string>();
        private readonly HashSet<string> _groupIds = new HashSet<string>();
        private readonly Dictionary<string, Panel> _chatPanels = new Dictionary<string, Panel>();
        private readonly string _avatarFolder = Path.Combine(Application.StartupPath, "UserAvatars");
        private const string FirebaseBaseUrl = FirebaseConfig.DatabaseUrl;
        private const long MaxFileSizeBytes = 2L * 1024 * 1024;

        private string _currentChatId = null;
        private bool _isGroupChat = false;
        public string CurrentUserUid { get; private set; }
        public string CurrentUserEmail { get; private set; }
        private bool _initialized = false;
        private System.Windows.Forms.Timer _pollTimer;

        private class FirebaseMessage { public string text, timestamp, peerUid, peerEmail, imageBase64, fileName, fileBase64, senderName; public bool isMine; public long fileSize; }
        private class ChatMessage { public string Text; public bool IsMine; public string ImageBase64, FileName, FileBase64; public long FileSize; public string SenderName; public string SenderUid; }
        private class FileBubbleTag { public string FileName, FileBase64; public long FileSize; }
        public UC_Message()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            this.BackColor = Color.White; // Nền backup
            this.Load += UC_Message_Load;

            btnSend.Click += btnSend_Click;
            btnEmoji.Click += btnEmoji_Click;
            btnImage.Click += btnImage_Click;
            btnAttach.Click += btnAttach_Click;
            if (btnMenu != null) btnMenu.Click += (s, e) => OnMenuClicked?.Invoke();

            // Giữ lại Voice Call
            if (btnVoiceCall != null) btnVoiceCall.Click += async (s, e) => await MakeCall("Voice");

            // Đã xóa phần btnVideoCall.Click...

            btnAddFriend.Click += async (s, e) => {
                using (var frm = new FormCustomInput("THÊM BẠN MỚI", "Nhập Email/Username..."))
                    if (frm.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(frm.ResultText))
                        try { await _dbService.SendFriendRequestAsync(CurrentUserUid, frm.ResultText.Trim()); MessageBox.Show("Đã gửi lời mời!"); } catch (Exception ex) { MessageBox.Show(ex.Message); }
            };

            btnAccept.Click += async (s, e) => {
                try
                {
                    var req = await _dbService.GetPendingRequestsAsync(CurrentUserUid);
                    if (req == null || req.Count == 0) { MessageBox.Show("Không có lời mời nào."); return; }
                    using (var frm = new FormRequestList(req))
                        if (frm.ShowDialog() == DialogResult.OK) { await _dbService.AcceptFriendRequestAsync(CurrentUserUid, frm.SelectedUid); MessageBox.Show("Đã kết bạn!"); await LoadChatList(); }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            };

            btnCreateGroup.Click += async (s, e) => {
                using (var frm = new FormCreateGroup(CurrentUserUid))
                    if (frm.ShowDialog() == DialogResult.OK) { MessageBox.Show("Tạo nhóm thành công!"); await LoadChatList(); }
            };

            if (btnMoreInfo != null) btnMoreInfo.Click += async (s, e) => {
                if (string.IsNullOrEmpty(_currentChatId)) return;
                string msg = _isGroupChat ? "Rời nhóm?" : "Xóa chat?";
                if (MessageBox.Show(msg, "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        if (_isGroupChat) await _dbService.LeaveGroupAsync(_currentChatId, CurrentUserUid);
                        else if (_idToUid.TryGetValue(_currentChatId, out string uid)) await _dbService.DeleteConversationAsync(CurrentUserUid, uid);
                        flChatList.Controls.RemoveByKey(_currentChatId); ResetRightPanel();
                    }
                    catch { }
                }
            };

            txtSearch.TextChanged += (s, e) => FilterChatList(txtSearch.Text);
            flMessages.SizeChanged += (s, e) => RelayoutAllRows();
            BuildEmojiMenu();
        }
        public void InitializeCurrentUser(string uid, string email) { CurrentUserUid = uid; CurrentUserEmail = email; _initialized = true; InitMessagePolling(); }
        private bool EnsureInitialized() => _initialized && !string.IsNullOrEmpty(CurrentUserUid);
        private async void UC_Message_Load(object sender, EventArgs e) => await LoadChatList();

        private void InitMessagePolling()
        {
            if (_pollTimer != null) { _pollTimer.Stop(); _pollTimer.Dispose(); }

            // [ĐÃ CHỈNH SỬA] Tăng Interval lên 3000 (3 giây)
            // Bạn có thể sửa thành 5000 (5 giây) nếu muốn lâu hơn nữa
            _pollTimer = new System.Windows.Forms.Timer { Interval = 3000 };

            _pollTimer.Tick += async (s, e) => {
                await CheckIncomingMessages();
                await UpdateFriendStatusUI();
                await _dbService.UpdateUserActivity(CurrentUserUid, true);
            };
            _pollTimer.Start();
        }
        private async Task UpdateFriendStatusUI()
        {
            try
            {
                var friends = await _dbService.GetFriendsAsync(CurrentUserUid);
                if (friends == null) return;
                foreach (var u in friends)
                {
                    if (string.IsNullOrEmpty(u.Email)) continue;

                    // Sync Avatar
                    if (!string.IsNullOrEmpty(u.AvatarBase64))
                    {
                        string localPath = Path.Combine(_avatarFolder, u.Uid + ".jpg");
                        try
                        {
                            if (!File.Exists(localPath))
                            {
                                byte[] b = Convert.FromBase64String(u.AvatarBase64);
                                File.WriteAllBytes(localPath, b);
                                if (_chatPanels.TryGetValue(u.Email, out Panel pp))
                                    foreach (Control c in pp.Controls) if (c is CircularPictureBox pc) using (var ms = new MemoryStream(b)) pc.Image = Image.FromStream(ms);
                            }
                        }
                        catch { }
                    }

                    if (_chatPanels.TryGetValue(u.Email, out Panel p))
                    {
                        bool isOnline = u.IsOnline;
                        if (isOnline && !string.IsNullOrEmpty(u.LastActive))
                        {
                            if (DateTime.TryParse(u.LastActive, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime t))
                                if ((DateTime.UtcNow - t).TotalMinutes > 2) isOnline = false;
                        }
                        foreach (Control c in p.Controls)
                        {
                            if (c is Label lbl && c.Location.Y > 30) { lbl.Text = isOnline ? "Online" : "Offline"; lbl.ForeColor = isOnline ? Color.LimeGreen : Color.Gray; }
                            if (c is Panel dot && c.Height < 20) dot.Visible = isOnline;
                        }
                        if (_currentChatId == u.Email && !_isGroupChat) lblChatStatus.Text = isOnline ? "Đang hoạt động" : "Truy cập " + GetTimeAgo(u.LastActive);
                    }
                }
            }
            catch { }
        }

        private string GetTimeAgo(string s) { if (string.IsNullOrEmpty(s)) return "gần đây"; if (DateTime.TryParse(s, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime t)) { var d = DateTime.UtcNow - t; if (d.TotalMinutes < 1) return "vừa xong"; if (d.TotalMinutes < 60) return (int)d.TotalMinutes + " phút trước"; return (int)d.TotalDays + " ngày trước"; } return "gần đây"; }
        private async Task LoadChatList()
            
        {
            if (!EnsureInitialized()) return;
            flChatList.Controls.Clear(); _idToUid.Clear(); _idToName.Clear(); _groupIds.Clear(); _chatPanels.Clear();
            try
            {
                var friends = await _dbService.GetFriendsAsync(CurrentUserUid);
                if (friends != null) foreach (var u in friends)
                    {
                        _idToUid[u.Email] = u.Uid; _idToName[u.Email] = u.Username ?? u.Email;
                        _chatPanels[u.Email] = AddChatItemToUI(u.Username ?? u.Email, u.IsOnline ? "Online" : "Offline", u.Email, u.Uid, false);
                    }
                var groups = await _dbService.GetUserGroupsAsync(CurrentUserUid);
                if (groups != null) foreach (var g in groups)
                    {
                        _groupIds.Add(g.GroupId); _idToName[g.GroupId] = g.Name;
                        _chatPanels[g.GroupId] = AddChatItemToUI(g.Name, "Nhóm", g.GroupId, null, true);
                    }
            }
            catch { }
        }

        private Panel AddChatItemToUI(string name, string status, string id, string uid, bool isGroup)
        {
            // Panel list item - Background Transparent để ăn theo màu Bookshelf
            Panel p = new Panel { Name = id, Size = new Size(flChatList.Width - 20, 72), Margin = new Padding(10, 5, 10, 5), BackColor = Color.Transparent, Cursor = Cursors.Hand, Tag = name };
            CircularPictureBox pic = new CircularPictureBox { Size = new Size(50, 50), Location = new Point(10, 11), SizeMode = PictureBoxSizeMode.StretchImage };
            if (isGroup) { Bitmap b = new Bitmap(50, 50); using (Graphics g = Graphics.FromImage(b)) { g.Clear(Color.SaddleBrown); g.DrawString("G", new Font("Arial", 20, FontStyle.Bold), Brushes.White, 10, 10); } pic.Image = b; }
            else { try { string pt = Path.Combine(_avatarFolder, uid + ".jpg"); if (File.Exists(pt)) pic.Image = Image.FromFile(pt); else pic.BackColor = Color.Peru; } catch { pic.BackColor = Color.Peru; } }
            p.Controls.Add(pic);
            Panel dot = new Panel { Size = new Size(14, 14), BackColor = Color.FromArgb(49, 162, 76), Location = new Point(48, 48), Visible = (status == "Online") };
            GraphicsPath gp = new GraphicsPath(); gp.AddEllipse(0, 0, 14, 14); dot.Region = new Region(gp);
            if (!isGroup) p.Controls.Add(dot); dot.BringToFront();

            // Text Color: White/Light Beige trên nền gỗ tối
            p.Controls.Add(new Label { Text = name, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.NavajoWhite, Location = new Point(70, 15), AutoSize = true });
            p.Controls.Add(new Label { Text = status, Font = new Font("Segoe UI", 9), ForeColor = (status == "Online" ? Color.LimeGreen : Color.Gray), Location = new Point(70, 40), AutoSize = true });

            // Hover Color: Gỗ sáng hơn chút
            p.MouseEnter += (s, e) => p.BackColor = Color.FromArgb(60, 40, 30);
            p.MouseLeave += (s, e) => p.BackColor = Color.Transparent;
            p.Click += (s, e) => SwitchConversation(id, isGroup);
            flChatList.Controls.Add(p); return p;
        }
        //------

        private async void SwitchConversation(string id, bool isGroup)
        {
            _currentChatId = id;
            _isGroupChat = isGroup;

            // Lấy tên hiển thị
            string displayName = _idToName.ContainsKey(id) ? _idToName[id] : id;
            lblChatName.Text = displayName;

            // Xử lý Avatar
            if (!isGroup && _idToUid.ContainsKey(id))
            {
                try
                {
                    string pt = Path.Combine(_avatarFolder, _idToUid[id] + ".jpg");
                    if (File.Exists(pt))
                        picChatAvatar.Image = Image.FromFile(pt);
                    else
                        picChatAvatar.BackColor = Color.Peru;
                }
                catch { picChatAvatar.BackColor = Color.Peru; }
            }
            else
            {
                // Avatar Nhóm (Vẽ chữ cái đầu)
                Bitmap b = new Bitmap(50, 50);
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.Clear(Color.SaddleBrown);
                    string firstLetter = string.IsNullOrEmpty(displayName) ? "G" : displayName.Substring(0, 1).ToUpper();
                    g.DrawString(firstLetter, new Font("Arial", 20, FontStyle.Bold), Brushes.White, 10, 10);
                }
                picChatAvatar.Image = b;
            }

            // --- [FIX LỖI MẤT TIN NHẮN KHI CLICK LẠI] ---
            if (_seenMessageIds.ContainsKey(id))
            {
                _seenMessageIds[id].Clear();
            }
            // ---------------------------------------------

            flMessages.Controls.Clear();
            await LoadHistory(id, isGroup);
        }

        private async Task LoadHistory(string id, bool isGroup)
        {
            try
            {
                string t = DateTime.Now.Ticks.ToString();
                string url = isGroup ? $"{FirebaseBaseUrl}/GroupMessages/{id}.json?t={t}" : $"{FirebaseBaseUrl}/Messages/{CurrentUserUid}.json?t={t}";
                string json = await Http.GetStringAsync(url);
                if (string.IsNullOrEmpty(json) || json == "null") { _conversations[id] = new List<ChatMessage>(); return; }
                var dict = JsonConvert.DeserializeObject<Dictionary<string, FirebaseMessage>>(json);
                var list = dict.Select(x => new { Key = x.Key, Val = x.Value }).OrderBy(x => x.Val.timestamp).ToList();

                if (!_seenMessageIds.ContainsKey(id)) _seenMessageIds[id] = new HashSet<string>();

                string targetUid = (!isGroup && _idToUid.ContainsKey(id)) ? _idToUid[id] : "";

                foreach (var item in list)
                {
                    if (_seenMessageIds[_currentChatId].Contains(item.Key)) continue;

                    if (!_isGroupChat && item.Val.peerUid != targetUid) continue; // Lọc tin nhắn theo UID

                    _seenMessageIds[id].Add(item.Key);
                    bool mine = isGroup ? (item.Val.peerUid == CurrentUserUid) : item.Val.isMine;
                    DisplayMessage(mine, item.Val.text, item.Val.imageBase64, item.Val.fileBase64, item.Val.fileName, item.Val.fileSize, isGroup ? item.Val.peerUid : "");
                }
                if (flMessages.Controls.Count > 0) flMessages.ScrollControlIntoView(flMessages.Controls[flMessages.Controls.Count - 1]);
            }
            catch { }
        }
        //--------------
        private async Task MakeCall(string type)
        {
            if (string.IsNullOrEmpty(_currentChatId)) return;
            try
            {
                string name = _isGroupChat ? $"Nhóm: {lblChatName.Text}" : CurrentUserEmail;
                if (_isGroupChat)
                {
                    var mems = await _dbService.GetGroupMemberUidsAsync(_currentChatId);
                    foreach (var u in mems) if (u != CurrentUserUid) await _dbService.SendCallRequestAsync(u, new CallInfo { CallerUid = CurrentUserUid, CallerName = name, Type = type, Status = "Dialing" });
                    using (var f = new FormVideoCall(lblChatName.Text, _currentChatId, CurrentUserUid, false, true, type)) f.ShowDialog();
                }
                else
                {
                    if (_idToUid.TryGetValue(_currentChatId, out string uid))
                    {
                        await _dbService.SendCallRequestAsync(uid, new CallInfo { CallerUid = CurrentUserUid, CallerName = name, Type = type, Status = "Dialing" });
                        using (var f = new FormVideoCall(lblChatName.Text, uid, CurrentUserUid, false, false, type)) f.ShowDialog();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMessage.Text) || string.IsNullOrEmpty(_currentChatId)) return;
            string txt = txtMessage.Text.Trim();
            txtMessage.Clear(); // Chỉ xóa text, không hiện local để tránh spam

            if (_isGroupChat) await _dbService.SendGroupMessageAsync(_currentChatId, CurrentUserUid, CurrentUserEmail, new FirebaseMessage { text = txt, timestamp = DateTime.UtcNow.ToString("o"), peerUid = CurrentUserUid, senderName = CurrentUserEmail });
            else if (_idToUid.TryGetValue(_currentChatId, out string uid)) await SendPrivateMessage(CurrentUserUid, uid, txt, null, null, 0);

            await CheckIncomingMessages();
        }
        //------------
        private async Task SendPrivateMessage(string f, string t, string txt, string i, string fi, long sz, string fn = null)
        {
            string ts = DateTime.UtcNow.ToString("o");
            var ms = new FirebaseMessage { text = txt, isMine = true, timestamp = ts, peerUid = t, peerEmail = _currentChatId, imageBase64 = i, fileBase64 = fi, fileName = fn, fileSize = sz };
            var mr = new FirebaseMessage { text = txt, isMine = false, timestamp = ts, peerUid = f, peerEmail = CurrentUserEmail, imageBase64 = i, fileBase64 = fi, fileName = fn, fileSize = sz };
            try { await Http.PostAsync($"{FirebaseBaseUrl}/Messages/{f}.json", new StringContent(JsonConvert.SerializeObject(ms), Encoding.UTF8, "application/json")); await Http.PostAsync($"{FirebaseBaseUrl}/Messages/{t}.json", new StringContent(JsonConvert.SerializeObject(mr), Encoding.UTF8, "application/json")); } catch { }
        }

        private async Task CheckIncomingMessages()
        {
            if (string.IsNullOrEmpty(CurrentUserUid)) return;
            try
            {
                string callUrl = $"{FirebaseBaseUrl}/CallRequests/{CurrentUserUid}.json?t={DateTime.Now.Ticks}";
                string callJson = await Http.GetStringAsync(callUrl);
                if (!string.IsNullOrEmpty(callJson) && callJson != "null")
                {
                    var call = JsonConvert.DeserializeObject<CallInfo>(callJson);
                    if (call != null && call.Status == "Dialing")
                    {
                        _pollTimer.Stop();
                        using (var f = new FormVideoCall(call.CallerName, call.CallerUid, CurrentUserUid, true, false, call.Type)) f.ShowDialog();
                        _pollTimer.Start();
                    }
                }
            }
            catch { }

            if (string.IsNullOrEmpty(_currentChatId)) return;
            try
            {
                string t = DateTime.Now.Ticks.ToString();
                string url = _isGroupChat ? $"{FirebaseBaseUrl}/GroupMessages/{_currentChatId}.json?t={t}" : $"{FirebaseBaseUrl}/Messages/{CurrentUserUid}.json?t={t}";
                string json = await Http.GetStringAsync(url);
                if (string.IsNullOrEmpty(json) || json == "null") return;
                var dict = JsonConvert.DeserializeObject<Dictionary<string, FirebaseMessage>>(json);
                var list = dict.Select(x => new { Key = x.Key, Val = x.Value }).OrderBy(x => x.Val.timestamp).ToList();

                if (!_seenMessageIds.ContainsKey(_currentChatId)) _seenMessageIds[_currentChatId] = new HashSet<string>();

                string targetUid = (!_isGroupChat && _idToUid.ContainsKey(_currentChatId)) ? _idToUid[_currentChatId] : "";

                foreach (var item in list)
                {
                    if (_seenMessageIds[_currentChatId].Contains(item.Key)) continue;

                    if (!_isGroupChat && item.Val.peerUid != targetUid) continue;

                    _seenMessageIds[_currentChatId].Add(item.Key);
                    bool mine = _isGroupChat ? (item.Val.peerUid == CurrentUserUid) : item.Val.isMine;
                    DisplayMessage(mine, item.Val.text, item.Val.imageBase64, item.Val.fileBase64, item.Val.fileName, item.Val.fileSize, _isGroupChat ? item.Val.peerUid : "");
                }
                if (flMessages.Controls.Count > 0) flMessages.ScrollControlIntoView(flMessages.Controls[flMessages.Controls.Count - 1]);
            }
            catch { }
        }
        //----
    }
}
