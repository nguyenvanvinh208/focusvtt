using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Net.Http; // Cần cho HttpClient
using System.Text;     // Cần cho Encoding
using System.Threading.Tasks;
using Newtonsoft.Json; // Thư viện JSON (thường đi kèm khi cài Firebase)
using Do_an.Models;

namespace Do_an.Firebase
{
    public class FirebaseAuthService
    {
        private readonly FirebaseAuthClient _authClient;
        private readonly FirebaseClient _dbClient;

        public FirebaseAuthService()
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = FirebaseConfig.ApiKey,
                AuthDomain = $"{FirebaseConfig.ProjectId}.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider(),
                    new GoogleProvider()
                },
                UserRepository = new FileUserRepository("FirebaseSample")
            };

            _authClient = new FirebaseAuthClient(config);
            _dbClient = new FirebaseClient(FirebaseConfig.DatabaseUrl);
        }

        // =====================
        // QUÊN MẬT KHẨU (Reset Password) - ĐÃ SỬA BẰNG API TRỰC TIẾP
        // =====================
        public async Task ResetPasswordAsync(string email)
        {
            try
            {
                // Sử dụng REST API của Firebase Auth để gửi yêu cầu reset
                // URL chuẩn của Google Identity Toolkit
                string requestUrl = $"https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key={FirebaseConfig.ApiKey}";

                using (var client = new HttpClient())
                {
                    // Tạo nội dung yêu cầu (JSON)
                    var payload = new
                    {
                        requestType = "PASSWORD_RESET",
                        email = email
                    };

                    string jsonPayload = JsonConvert.SerializeObject(payload);
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    // Gửi yêu cầu POST
                    var response = await client.PostAsync(requestUrl, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        // Đọc lỗi từ Google trả về
                        string errorContent = await response.Content.ReadAsStringAsync();

                        if (errorContent.Contains("EMAIL_NOT_FOUND"))
                            throw new Exception("Email này chưa được đăng ký!");
                        else if (errorContent.Contains("INVALID_EMAIL"))
                            throw new Exception("Định dạng email không hợp lệ!");
                        else
                            throw new Exception("Lỗi từ hệ thống: " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // =====================
        // ĐĂNG KÝ (GIỮ NGUYÊN)
        // =====================
        public async Task<UserCredential> RegisterAsync(string username, string email, string password)
        {
            try
            {
                var auth = await _authClient.CreateUserWithEmailAndPasswordAsync(email, password);

                var user = new Models.User
                {
                    Uid = auth.User.Uid,
                    Username = username,
                    Email = email,
                    CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };

                await _dbClient
                    .Child("Users")
                    .Child(auth.User.Uid)
                    .PutAsync(user);

                return auth;
            }
            catch (FirebaseAuthException ex)
            {
                string message = ex.Reason switch
                {
                    AuthErrorReason.EmailExists => "Email đã được sử dụng.",
                    AuthErrorReason.WeakPassword => "Mật khẩu quá yếu.",
                    _ => "Đăng ký thất bại."
                };
                throw new Exception(message);
            }
        }

        // =====================
        // LOGIN & LOGOUT (GIỮ NGUYÊN)
        // =====================
        private async Task<string> GetEmailByUsernameAsync(string username)
        {
            var users = await _dbClient.Child("Users").OnceAsync<Models.User>();
            foreach (var user in users)
            {
                if (user.Object.Username != null &&
                    user.Object.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
                    return user.Object.Email;
            }
            return null;
        }

        public async Task<UserCredential> LoginByUsernameAsync(string username, string password)
        {
            try
            {
                var email = await GetEmailByUsernameAsync(username);
                if (string.IsNullOrEmpty(email))
                    throw new Exception("Tài khoản không tồn tại!");

                var auth = await _authClient.SignInWithEmailAndPasswordAsync(email, password);
                await _dbClient.Child("Users").Child(auth.User.Uid).Child("IsOnline").PutAsync(true);
                return auth;
            }
            catch (FirebaseAuthException ex)
            {
                string message = ex.Reason switch
                {
                    AuthErrorReason.WrongPassword => "Sai mật khẩu!",
                    AuthErrorReason.UnknownEmailAddress => "Tài khoản không tồn tại!",
                    _ => "Đăng nhập thất bại!"
                };
                throw new Exception(message);
            }
        }

        public async Task LogoutAsync(string uid)
        {
            try
            {
                if (!string.IsNullOrEmpty(uid))
                {
                    await _dbClient.Child("Users").Child(uid).Child("IsOnline").PutAsync(false);
                }
                _authClient.SignOut();
            }
            catch (Exception ex)
            {
                throw new Exception("Đăng xuất thất bại: " + ex.Message);
            }
        }
    }
}