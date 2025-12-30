using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Do_an.Firebase;

namespace Do_an.Forms
{
    public partial class signin : Form
    {
        private readonly Login loginForm;
        private readonly FirebaseAuthService _authService;
        private const string PH_USER = "Tên đăng nhập";
        private const string PH_EMAIL = "Địa chỉ Email";
        private const string PH_PASS = "Mật khẩu";
        private const string PH_CONFIRM = "Nhập lại mật khẩu";

        public signin()
        {
            InitializeComponent();
            InitializeCustomLogic();
        }

        public signin(Login loginForm)
        {
            InitializeComponent();
            this.loginForm = loginForm;
            _authService = new FirebaseAuthService();
            InitializeCustomLogic();
        }

        private void InitializeCustomLogic()
        {
            SetRoundedRegion(this, 30);
            SetRoundedRegion(pictureBox1, 30);
            SetRoundedRegion(pnlContainer, 30);
            SetRoundedRegion(btnSignup, 20);

            SetupPlaceholder(txtUsername, PH_USER);
            SetupPlaceholder(txtEmail, PH_EMAIL);
            SetupPlaceholder(txtPassword, PH_PASS, true);
            SetupPlaceholder(txtConfirmPassword, PH_CONFIRM, true);
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

        private async void btnSignup_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || username == PH_USER ||
                string.IsNullOrEmpty(email) || email == PH_EMAIL ||
                string.IsNullOrEmpty(password) || password == PH_PASS)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                await _authService.RegisterAsync(username, email, password);
                MessageBox.Show("Đăng ký thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
                if (loginForm != null) loginForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = chkShowPassword.Checked;
            if (txtPassword.Text != PH_PASS) txtPassword.UseSystemPasswordChar = !isChecked;
            if (txtConfirmPassword.Text != PH_CONFIRM) txtConfirmPassword.UseSystemPasswordChar = !isChecked;
        }

        private void llbLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
            if (loginForm != null) loginForm.Show();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void lblTitle_Click(object sender, EventArgs e) { }

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