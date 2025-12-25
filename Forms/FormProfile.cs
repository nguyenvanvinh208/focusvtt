using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using Do_an.Models;
using Do_an.Firebase;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Do_an.Forms
{
    public partial class FormProfile : Form
    {
        private User _user;
        private FirebaseDatabaseService _dbService;
        private bool _isEditMode = false;

        private Label lblBio;
        private TextBox txtEditName;
        private TextBox txtEditBio;

        public FormProfile(User user)
        {
            _user = user;
            _dbService = new FirebaseDatabaseService();
            InitializeComponent();

            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Tạo Label Bio nếu chưa có trong Designer
            if (lblBio == null)
            {
                lblBio = new Label();
                lblBio.ForeColor = Color.LightGray;
                lblBio.Font = new Font("Segoe UI", 11, FontStyle.Italic);
                lblBio.AutoSize = true;
                this.Controls.Add(lblBio);
            }

            // Setup giao diện ban đầu
            LoadDataToUI();
            ApplyCustomLayout();
            SetupStatsPanel();
            SetupEditPanel();

            // [QUAN TRỌNG] Lấy dữ liệu MỚI NHẤT từ Server
            _ = RefreshDataFromServer();
        }

        // --- HÀM LẤY DỮ LIỆU TỪ SERVER (REALTIME) ---
        private async Task RefreshDataFromServer()
        {
            try
            {
                // 1. [QUAN TRỌNG] Gọi hàm tính toán Streak trên Server trước
                // Để đảm bảo nếu hôm nay vừa đăng nhập thì Streak sẽ tăng lên 1
                await _dbService.CheckAndUpdateStreakAsync(_user.Uid);

                // 2. Tải thông tin User mới nhất về
                var latestUser = await _dbService.GetUserAsync(_user.Uid);

                if (latestUser != null)
                {
                    _user.Username = latestUser.Username;
                    _user.Info = latestUser.Info ?? new UserProfile();
                }

                // 3. Cập nhật lên màn hình
                if (!this.IsDisposed && this.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        LoadDataToUI();
                        SetupStatsPanel();   // Vẽ lại thẻ với dữ liệu mới
                        SetupEditPanel();
                        ApplyCustomLayout();
                    });
                }
            }
            catch { /* Bỏ qua lỗi mạng */ }
        }

        // --- SETUP CÁC THẺ THỐNG KÊ (PROFILE) ---
        private void SetupStatsPanel()
        {
            pnlStats.Controls.Clear();

            int totalWidth = pnlStats.Width;
            int gap = 20;
            int totalGap = gap * 2;
            int cardWidth = (totalWidth - totalGap) / 3;
            int cardHeight = pnlStats.Height;

            // --- THẺ 1: CHUỖI HIỆN TẠI ---
            StatCard c1 = new StatCard()
            {
                Title = "Chuỗi hiện tại",
                Value = $"{_user.Info.CurrentStreak} NGÀY",
                Unit = "STREAK 🔥",
                ColorStart = Color.FromArgb(255, 60, 0),    // Cam
                ColorEnd = Color.FromArgb(255, 100, 50),
                Size = new Size(cardWidth, cardHeight),
                Location = new Point(0, 0)
            };

            // --- THẺ 2: CHUỖI TỐT NHẤT ---
            StatCard c2 = new StatCard()
            {
                Title = "Chuỗi tốt nhất",
                Value = $"{_user.Info.BestStreak} NGÀY",
                Unit = "KỶ LỤC 🏆",
                ColorStart = Color.FromArgb(255, 140, 0),   // Vàng
                ColorEnd = Color.FromArgb(255, 180, 0),
                Size = new Size(cardWidth, cardHeight),
                Location = new Point(cardWidth + gap, 0)
            };

            // --- THẺ 3: TỔNG THỜI GIAN (Hiển thị Giờ:Phút:Giây) ---
            // Chuyển đổi số giờ (double) thành TimeSpan để hiển thị chi tiết
            TimeSpan ts = TimeSpan.FromHours(_user.Info.TotalHours);
            string timeString = $"{(int)ts.TotalHours:00}:{ts.Minutes:00}:{ts.Seconds:00}";

            StatCard c3 = new StatCard()
            {
                Title = "Tổng thời gian",
                Value = timeString,
                Unit = "THỜI GIAN ⏳",
                ColorStart = Color.FromArgb(0, 180, 80),    // Xanh lá
                ColorEnd = Color.FromArgb(50, 220, 100),
                Size = new Size(cardWidth, cardHeight),
                Location = new Point((cardWidth + gap) * 2, 0)
            };

            pnlStats.Controls.AddRange(new Control[] { c1, c2, c3 });
        }

        // --- CÁC HÀM UI KHÁC GIỮ NGUYÊN ---
        private void ApplyCustomLayout()
        {
            foreach (Control c in this.Controls)
            {
                if (c.Name == "lblClose") c.Location = new Point(this.Width - 40, 10);
                if (c.Name == "btnAction") c.Location = new Point(this.Width - 140, 50);
                if (c.Name == "btnCancel") c.Location = new Point(this.Width - 250, 50);
            }

            picAvatar.Location = new Point(50, 50);
            picAvatar.Size = new Size(120, 120);

            lblName.Location = new Point(190, 50);
            lblBio.Location = new Point(190, 90);
            lblLevel.Location = new Point(190, 125);
            lblXP.Location = new Point(250, 127);

            line.Size = new Size(this.Width - 100, 1);
            line.Location = new Point(50, 190);

            int startY = line.Location.Y + 30;
            int fixedHeight = 250;

            pnlStats.Location = new Point(50, startY);
            pnlStats.Size = new Size(this.Width - 100, fixedHeight);

            pnlEdit.Location = new Point(50, startY);
            pnlEdit.Size = new Size(this.Width - 100, 300);
        }

        private void SetupEditPanel()
        {
            pnlEdit.Controls.Clear();
            int textWidth = pnlEdit.Width - 50;

            Label l1 = new Label() { Text = "Tên hiển thị:", ForeColor = Color.White, Location = new Point(0, 0), AutoSize = true };
            txtEditName = new TextBox() { Width = textWidth, Font = new Font("Segoe UI", 12), Location = new Point(0, 30), BackColor = Color.FromArgb(50, 50, 60), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle };

            Label l2 = new Label() { Text = "Mô tả / Mục tiêu:", ForeColor = Color.White, Location = new Point(0, 80), AutoSize = true };
            txtEditBio = new TextBox() { Width = textWidth, Height = 180, Multiline = true, Font = new Font("Segoe UI", 11), Location = new Point(0, 110), BackColor = Color.FromArgb(50, 50, 60), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle };

            pnlEdit.Controls.AddRange(new Control[] { l1, txtEditName, l2, txtEditBio });
        }

        private void LoadDataToUI()
        {
            lblName.Text = _user.Username.ToUpper();
            lblLevel.Text = $"Lv. {_user.Info.Level}";
            lblXP.Text = $"{_user.Info.XP} / {_user.Info.XPToNextLevel} XP";
            if (lblBio != null) lblBio.Text = $"\"{_user.Info.Bio}\"";

            string avatarPath = Path.Combine(Application.StartupPath, "UserAvatars", $"{_user.Uid}.jpg");
            if (File.Exists(avatarPath))
            {
                try
                {
                    using (var fs = new FileStream(avatarPath, FileMode.Open, FileAccess.Read))
                    {
                        picAvatar.Image = Image.FromStream(fs);
                    }
                }
                catch { }
            }
            else
            {
                if (Properties.Resources.profile != null)
                {
                    using (MemoryStream ms = new MemoryStream(Properties.Resources.profile))
                    {
                        picAvatar.Image = Image.FromStream(ms);
                    }
                }
            }

            if (txtEditName != null) txtEditName.Text = _user.Username;
            if (txtEditBio != null) txtEditBio.Text = _user.Info.Bio;
        }

        private void LblClose_Click(object sender, EventArgs e) => this.Close();
        private void BtnCancel_Click(object sender, EventArgs e) => ToggleEditMode(false);

        private async void BtnAction_Click(object sender, EventArgs e)
        {
            if (!_isEditMode)
            {
                ToggleEditMode(true);
            }
            else
            {
                try
                {
                    btnAction.Text = "Đang lưu...";
                    btnAction.Enabled = false;

                    await _dbService.UpdateUserProfileAsync(_user.Uid, txtEditName.Text, txtEditBio.Text);

                    _user.Username = txtEditName.Text;
                    _user.Info.Bio = txtEditBio.Text;

                    MessageBox.Show("Cập nhật thành công!");
                    ToggleEditMode(false);
                    // Load lại để chắc chắn hiển thị đúng cái vừa lưu
                    _ = RefreshDataFromServer();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
                finally
                {
                    btnAction.Enabled = true;
                }
            }
        }

        private void ToggleEditMode(bool isEdit)
        {
            _isEditMode = isEdit;
            pnlStats.Visible = !isEdit;
            pnlEdit.Visible = isEdit;
            btnCancel.Visible = isEdit;
            if (lblBio != null) lblBio.Visible = !isEdit;

            if (isEdit)
            {
                btnAction.Text = "CẬP NHẬT";
                btnAction.BackColor = Color.DodgerBlue;
            }
            else
            {
                btnAction.Text = "Chỉnh sửa";
                btnAction.BackColor = Color.FromArgb(40, 40, 50);
            }
        }

        private void PicAvatar_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddEllipse(0, 0, picAvatar.Width - 1, picAvatar.Height - 1);
                picAvatar.Region = new Region(gp);
                using (Pen p = new Pen(Color.Cyan, 3))
                    g.DrawEllipse(p, 1, 1, picAvatar.Width - 3, picAvatar.Height - 3);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                    Color.FromArgb(10, 10, 20), Color.FromArgb(20, 15, 35), 90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
            using (Pen p = new Pen(Color.FromArgb(60, 60, 70), 2))
                e.Graphics.DrawRectangle(p, 0, 0, Width - 1, Height - 1);
        }
    }
}