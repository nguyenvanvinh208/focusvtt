using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Do_an.Models;

namespace Do_an.Firebase
{
    public class FirebaseDatabaseService
    {
        private readonly FirebaseClient _firebaseClient;

        public FirebaseDatabaseService()
        {
            _firebaseClient = new FirebaseClient(FirebaseConfig.DatabaseUrl);
        }

        // 🔍 Lấy thông tin 1 user theo UID
        public async Task<User> GetUserAsync(string uid)
        {
            try
            {
                var user = await _firebaseClient
                    .Child("Users")
                    .Child(uid)
                    .OnceSingleAsync<User>();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy dữ liệu người dùng: {ex.Message}");
            }
        }

        // 📋 Lấy toàn bộ danh sách user
        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _firebaseClient
                .Child("Users")
                .OnceAsync<User>();

            return users.Select(item => item.Object).ToList();
        }

        //Hàm AddTaskAsync 
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
        //Ham GetTasksByDateAsync
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
        //Ham UpdateTaskStatusAsync
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
