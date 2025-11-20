using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Do_an.Firebase;

namespace Do_an.Forms
{
    public partial class Login : Form
    {
        private readonly FirebaseAuthService _authService;

        // KHAI BÁO VĂN BẢN HƯỚNG DẪN (Placeholder)
        private const string PH_USER = "Tên tài khoản hoặc Email";
        private const string PH_PASS = "Mật khẩu";

        public Login()
        {
            InitializeComponent();
            _authService = new FirebaseAuthService();

            // Đăng ký sự kiện Load để kiểm tra đăng nhập tự động
            this.Load += new System.EventHandler(this.Login_Load);

            // --- 1. CẤU HÌNH BO TRÒN ---
            SetRoundedRegion(this, 30);
            SetRoundedRegion(pictureBox1, 30);
            SetRoundedRegion(pnlContainer, 30);
            SetRoundedRegion(btnLogin, 20);

            // --- 2. CẤU HÌNH PLACEHOLDER ---
            SetupPlaceholder(txtUsername, PH_USER);
            SetupPlaceholder(txtPassword, PH_PASS, isPassword: true);
        }

        // --- SỰ KIỆN LOAD FORM: KIỂM TRA TỰ ĐỘNG ĐĂNG NHẬP ---
        private void Login_Load(object sender, EventArgs e)
        {
            // Nếu trong máy đã lưu trạng thái đăng nhập = true
            if (Properties.Settings.Default.IsLoggedIn)
            {
                string savedUid = Properties.Settings.Default.UserUID;

                // Mở thẳng form Main
                main mainForm = new main(this);
                mainForm.currentUserUid = savedUid;
                mainForm.Show();

                // Ẩn form Login đi (không đóng hẳn, chỉ ẩn)
                // Để khi logout còn hiện lại được
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                this.Hide();
            }
        }

        // --- HÀM XỬ LÝ PLACEHOLDER ---
        private void SetupPlaceholder(TextBox txt, string placeholderText, bool isPassword = false)
        {
            txt.Text = placeholderText;
            txt.ForeColor = Color.Gray;
            if (isPassword) txt.UseSystemPasswordChar = false;

            txt.Enter += (s, e) =>
            {
                if (txt.Text == placeholderText)
                {
                    txt.Text = "";
                    txt.ForeColor = Color.Black;
                    if (isPassword) txt.UseSystemPasswordChar = true;
                }
            };

            txt.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    txt.Text = placeholderText;
                    txt.ForeColor = Color.Gray;
                    if (isPassword) txt.UseSystemPasswordChar = false;
                }
            };
        }

        // SỰ KIỆN ĐĂNG NHẬP
        private async void btnLogin_Click_1(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || username == PH_USER ||
                string.IsNullOrEmpty(password) || password == PH_PASS)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên tài khoản và mật khẩu!",
                  "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var auth = await _authService.LoginByUsernameAsync(username, password);
                string uid = auth.User.Uid;

                // --- LƯU TRẠNG THÁI ĐĂNG NHẬP ---
                Properties.Settings.Default.IsLoggedIn = true;
                Properties.Settings.Default.UserUID = uid;
                Properties.Settings.Default.Save();
                // -------------------------------

                MessageBox.Show($"Đăng nhập thành công!\nXin chào: {auth.User.Info.Email}",
                  "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                main mainForm = new main(this);
                mainForm.currentUserUid = uid;
                mainForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đăng nhập thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkShowPassword_CheckedChanged_1(object sender, EventArgs e)
        {
            if (txtPassword.Text != PH_PASS)
            {
                if (chkShowPassword.Checked)
                    txtPassword.UseSystemPasswordChar = false;
                else
                    txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void llbQuenMatKhau_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            forgotpassword a = new forgotpassword(this);
            a.Show();
            this.Hide();
        }

        private void llbRegister_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            signin a = new signin(this);
            a.Show();
            this.Hide();
        }

        // Nút X ở Login -> Thoát hẳn App
        private void lblClose_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SetRoundedRegion(Control c, int radius)
        {
            Rectangle bounds = new Rectangle(0, 0, c.Width, c.Height);
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
            path.AddArc(bounds.X + bounds.Width - radius, bounds.Y, radius, radius, 270, 90);
            path.AddArc(bounds.X + bounds.Width - radius, bounds.Y + bounds.Height - radius, radius, radius, 0, 90);
            path.AddArc(bounds.X, bounds.Y + bounds.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            c.Region = new Region(path);
        }
    }
}