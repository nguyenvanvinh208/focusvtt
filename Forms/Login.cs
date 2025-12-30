using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Do_an.Firebase;
using Do_an.Models; 

namespace Do_an.Forms
{
    public partial class Login : Form
    {
        private readonly FirebaseAuthService _authService;

        private const string PH_USER = "Tên tài khoản hoặc Email";
        private const string PH_PASS = "Mật khẩu";

        public Login()
        {
            InitializeComponent();
            _authService = new FirebaseAuthService();

            this.Load += new System.EventHandler(this.Login_Load);

            SetRoundedRegion(this, 30);
            SetRoundedRegion(pictureBox1, 30);
            SetRoundedRegion(pnlContainer, 30);
            SetRoundedRegion(btnLogin, 20);

            SetupPlaceholder(txtUsername, PH_USER);
            SetupPlaceholder(txtPassword, PH_PASS, isPassword: true);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.IsLoggedIn)
            {
                string savedUid = Properties.Settings.Default.UserUID;
                User currentUser = new User
                {
                    Uid = savedUid,
                    Username = "Welcome Back" 
                };


                MainDashboard dashboard = new MainDashboard(currentUser);
                dashboard.Show();


                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                this.Hide();
            }
        }

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

                Properties.Settings.Default.IsLoggedIn = true;
                Properties.Settings.Default.UserUID = uid;
                Properties.Settings.Default.Save();
     

                MessageBox.Show($"Đăng nhập thành công!\nXin chào: {auth.User.Info.Email}",
                  "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);


                User currentUser = new User
                {
                    Uid = uid,
                    Username = username,
                    Email = auth.User.Info.Email,
                    Info = new UserProfile { Level = 1, XP = 0 }
                };

                MainDashboard dashboard = new MainDashboard(currentUser);
                dashboard.Show();

                this.Hide();
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