using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Do_an.Firebase;

namespace Do_an.Forms
{
    public partial class forgotpassword : Form
    {
        private readonly Login loginform;
        private readonly FirebaseAuthService _authService;

        // Text hướng dẫn
        private const string PH_EMAIL = "Nhập email đăng ký...";

        public forgotpassword(Login loginform)
        {
            InitializeComponent();
            this.loginform = loginform;
            _authService = new FirebaseAuthService();

            // Bo tròn giao diện
            SetRoundedRegion(this, 30);
            SetRoundedRegion(pictureBox1, 30);
            SetRoundedRegion(pnlContainer, 30);
            SetRoundedRegion(btnSend, 20);

            // Placeholder ban đầu
            SetupPlaceholder();
        }

        // --- Cấu hình Placeholder ---
        private void SetupPlaceholder()
        {
            txtEmail.Text = PH_EMAIL;
            txtEmail.ForeColor = Color.Gray;

            txtEmail.Enter += (s, e) =>
            {
                if (txtEmail.Text == PH_EMAIL)
                {
                    txtEmail.Text = "";
                    txtEmail.ForeColor = Color.Black;
                }
            };

            txtEmail.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    txtEmail.Text = PH_EMAIL;
                    txtEmail.ForeColor = Color.Gray;
                }
            };
        }

        // --- SỰ KIỆN GỬI MAIL ---
        private async void btnSend_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            // 1. Kiểm tra rỗng hoặc vẫn là text hướng dẫn
            if (string.IsNullOrEmpty(email) || email == PH_EMAIL)
            {
                MessageBox.Show("Vui lòng nhập địa chỉ email.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra định dạng email cơ bản
            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Định dạng email không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Gọi hàm đã sửa bên Service
                await _authService.ResetPasswordAsync(email);

                MessageBox.Show("Đã gửi email đặt lại mật khẩu thành công!\nVui lòng kiểm tra hộp thư đến (hoặc Spam).",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Gửi xong quay lại Login
                this.Close();
                if (loginform != null) loginform.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi Gửi Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llbBackToLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
            if (loginform != null) loginform.Show();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Thoát toàn bộ chương trình
        }

        private void forgotpassword_FormClosing(object sender, FormClosingEventArgs e) { }

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}