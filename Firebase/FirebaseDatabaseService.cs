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
    }
}
