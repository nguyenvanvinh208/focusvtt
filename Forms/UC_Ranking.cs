using Do_an.Firebase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Do_an.Forms
{
    public partial class UC_Ranking : UserControl
    {
        private User _currentUser;
        private FirebaseDatabaseService _dbService;
        private List<User> _allUsers = new List<User>();
        private System.Windows.Forms.Timer _refreshTimer;

        private bool _isGlobal = true;

        private readonly Color clrWoodDark = Color.FromArgb(60, 40, 20);    // Màu gỗ tối (Nền)
        private readonly Color clrWoodLight = Color.FromArgb(160, 100, 50); // Màu gỗ sáng (Bục Rank 2,3)
        private readonly Color clrRedRoyal = Color.FromArgb(120, 0, 30);    // Màu đỏ đô (Bục Rank 1)
        private readonly Color clrGold = Color.FromArgb(255, 215, 0);       // Màu Vàng Kim
        private readonly Color clrSilver = Color.FromArgb(192, 192, 192);   // Màu Bạc

        private int _contentWidth = 1000;
        public event Action OnMenuClicked;

        public UC_Ranking()
        {
            InitializeComponent();
            _currentUser = user;
            _dbService = new FirebaseDatabaseService();

            // Cấu hình vẽ mượt (Double Buffering) - Rất quan trọng khi vẽ nhiều hình ảnh
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);

            // --- [CẬP NHẬT] SET HÌNH NỀN TỪ RESOURCE 'ranking' ---
            try
            {
                // Kiểm tra xem resource có tồn tại không trước khi gán
                object bgObj = Properties.Resources.ResourceManager.GetObject("ranking");
                if (bgObj != null)
                {
                    this.BackgroundImage = ByteToImg(bgObj);
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }
                else
                {
                    this.BackColor = clrWoodDark; // Màu fallback nếu quên add hình
                }
            }
            catch
            {
                this.BackColor = clrWoodDark;
            }

            // Gán sự kiện nút Tab
            SetupTabButtons();

        }
        //------
        private void UC_Ranking_Load(object sender, EventArgs e)
        {
            SwitchTab(true);
            ResponsiveLayout();

            // Timer tự động cập nhật BXH mỗi 5 giây
            _refreshTimer = new System.Windows.Forms.Timer();
            _refreshTimer.Interval = 5000;
            _refreshTimer.Tick += async (s, ev) => await LoadData();
            _refreshTimer.Start();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (_refreshTimer != null) { _refreshTimer.Stop(); _refreshTimer.Dispose(); }
            base.OnHandleDestroyed(e);
        }

        private void SetupTabButtons()
        {
            btnTabGlobal.Click += (s, e) => SwitchTab(true);
            btnTabFriends.Click += (s, e) => SwitchTab(false);
        }
        //-----
        private async void SwitchTab(bool isGlobal)
        {
            _isGlobal = isGlobal;
            if (isGlobal)
            {
                btnTabGlobal.BackColor = clrWoodLight;
                btnTabGlobal.ForeColor = Color.White;
                btnTabFriends.BackColor = Color.Transparent;
                btnTabFriends.ForeColor = Color.Gray;
            }
            else
            {
                btnTabFriends.BackColor = clrWoodLight;
                btnTabFriends.ForeColor = Color.White;
                btnTabGlobal.BackColor = Color.Transparent;
                btnTabGlobal.ForeColor = Color.Gray;
            }
            await LoadData();
        }

        // --- TẢI DỮ LIỆU TỪ FIREBASE ---
        private async Task LoadData()
        {
            try
            {
                // Cập nhật rank của bản thân trước
                await _dbService.CalculateAndSaveRankAsync(_currentUser.Uid);
                var updatedMe = await _dbService.GetUserAsync(_currentUser.Uid);
                if (updatedMe != null) _currentUser = updatedMe;

                List<User> data;
                if (_isGlobal)
                {
                    data = await _dbService.GetAllUsersAsync();
                }
                else
                {
                    data = await _dbService.GetFriendsAsync(_currentUser.Uid);
                    // Đảm bảo mình luôn có trong list bạn bè để so sánh
                    if (!data.Any(u => u.Uid == _currentUser.Uid)) data.Add(_currentUser);
                }

                // Sắp xếp: Giờ học giảm dần -> Level giảm dần
                _allUsers = data.OrderByDescending(u => u.Info.TotalHours)
                                .ThenByDescending(u => u.Info.Level)
                                .ToList();

                // Vẽ lại giao diện
                if (!this.IsDisposed && this.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        pnlPodium.Invalidate();
                        DrawList();
                        DrawMyRank();
                        ResponsiveLayout();
                    });
                }
            }
            catch (Exception) { }
        }

        //-----

        // PHẦN VẼ BỤC VINH QUANG (PODIUM) - ĐÃ FIX LỖI LAYER & AVATAR

        private void pnlPodium_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int cx = pnlPodium.Width / 2;

            // Vẽ theo thứ tự: Rank 2 (Trái) -> Rank 3 (Phải) -> Rank 1 (Giữa)
            if (_allUsers.Count > 1) DrawPlaque(g, 2, _allUsers[1], cx - 220, 160, clrWoodLight);
            if (_allUsers.Count > 2) DrawPlaque(g, 3, _allUsers[2], cx + 220, 160, clrWoodLight);
            if (_allUsers.Count > 0) DrawPlaque(g, 1, _allUsers[0], cx, 190, clrRedRoyal);
        }

        private void DrawPlaque(Graphics g, int rank, User user, int centerX, int boxSize, Color baseColor)
        {
            // 1. TÍNH TOÁN TỌA ĐỘ
            int boxTopY = 130;
            if (rank == 1) boxTopY = 100; // Rank 1 cao hơn

            // Hình chữ nhật của cái hộp
            Rectangle rectBox = new Rectangle(centerX - (boxSize / 2), boxTopY, boxSize, boxSize);

            // Kích thước Avatar
            int avaSize = (rank == 1) ? 80 : 65;
            int avaX = centerX - (avaSize / 2);
            int avaY = boxTopY - (avaSize / 2); // Avatar nằm giữa cạnh trên của hộp

            using (Pen pLine = new Pen(Color.FromArgb(50, 255, 255, 255), 1))
            {
                g.DrawLine(pLine, centerX, 0, centerX, boxTopY);
            }

            // Vẽ Hộp (Plaque)
            using (GraphicsPath path = GetRoundedPath(rectBox, 15))
            {
                // Gradient màu nền hộp
                using (LinearGradientBrush b = new LinearGradientBrush(rectBox, ControlPaint.Light(baseColor), baseColor, 90F))
                {
                    g.FillPath(b, path);
                }
                // Viền hộp màu vàng
                using (Pen p = new Pen(clrGold, 2))
                {
                    g.DrawPath(p, path);
                }
            }
            // LAYER 2: VẼ SỐ HẠNG & THÔNG TIN TRÊN HỘP

            // Vẽ Số Hạng (1, 2, 3) nằm giữa hộp
            using (Font f = new Font("Georgia", 55, FontStyle.Bold))
            {
                string rankStr = rank.ToString();
                SizeF size = g.MeasureString(rankStr, f);
                float textX = centerX - (size.Width / 2);
                float textY = boxTopY + (boxSize / 2) - (size.Height / 2) + 10; // Dịch xuống 1 chút

                // Bóng chữ đen
                g.DrawString(rankStr, f, Brushes.Black, textX + 3, textY + 3);
                // Chữ chính màu vàng
                using (SolidBrush bText = new SolidBrush(clrGold))
                    g.DrawString(rankStr, f, bText, textX, textY);
            }

            // Vẽ Thời gian (Dưới đáy hộp, bên ngoài)
            TimeSpan ts = TimeSpan.FromHours(user.Info.TotalHours);
            string timeStr = $"{ts.TotalHours:00}:{ts.Minutes:00}:{ts.Seconds:00}";
            using (Font fTime = new Font("Consolas", 10, FontStyle.Bold))
            {
                SizeF sTime = g.MeasureString(timeStr, fTime);
                g.DrawString(timeStr, fTime, Brushes.WhiteSmoke, centerX - (sTime.Width / 2), boxTopY + boxSize + 5);
            }

            // LAYER 3: VẼ AVATAR (NỔI LÊN TRÊN HỘP) - VẼ SAU CÙNG ĐỂ KHÔNG BỊ CHE

            Image avatarImg = GetAvatar(user.Uid, user.Username);

            // 1. Vẽ bóng đổ sau lưng Avatar (để tách biệt với hộp)
            using (SolidBrush shadow = new SolidBrush(Color.FromArgb(100, 0, 0, 0)))
            {
                g.FillEllipse(shadow, avaX + 2, avaY + 2, avaSize, avaSize);
            }

            // 2. Vẽ Avatar (Cắt hình tròn)
            GraphicsPath pathAva = new GraphicsPath();
            pathAva.AddEllipse(avaX, avaY, avaSize, avaSize);

            g.SetClip(pathAva);
            g.DrawImage(avatarImg, avaX, avaY, avaSize, avaSize);
            g.ResetClip();

            // 3. Vẽ Viền Avatar
            Color borderCol = (rank == 1) ? clrGold : Color.Silver;
            using (Pen pAva = new Pen(borderCol, 3))
            {
                g.DrawEllipse(pAva, avaX, avaY, avaSize, avaSize);
            }

            // LAYER 4: VẼ PHỤ KIỆN & TÊN 

            if (rank == 1)
            {
                // Vương miện: Vẽ cao hơn Avatar
                using (Font fIcon = new Font("Segoe UI Emoji", 28))
                    g.DrawString("👑", fIcon, Brushes.Gold, centerX - 28, avaY - 55);

                // Ổ khóa (trang trí)
                using (Font fIcon = new Font("Segoe UI Emoji", 16))
                    g.DrawString("🔒", fIcon, Brushes.White, centerX - 12, boxTopY + 5);
            }
            else
            {
                // Vòng hào quang (Halo)
                using (Pen pRing = new Pen(Color.Gold, 2))
                    g.DrawEllipse(pRing, centerX - 20, avaY - 20, 40, 12);
            }

            // Tên User: Vẽ trên cùng
            string uName = (user.Username ?? "Unknown").ToUpper();
            if (uName.Length > 12) uName = uName.Substring(0, 10) + "..";
            using (Font fName = new Font("Segoe UI", 10, FontStyle.Bold))
            {
                SizeF sName = g.MeasureString(uName, fName);
                // Vẽ tên màu trắng
                g.DrawString(uName, fName, Brushes.White, centerX - (sName.Width / 2), avaY - 40);
            }
        }

        //--------------------
        // PHẦN VẼ DANH SÁCH (LIST)
        private Panel CreateRowItem(int rank, User user, bool isMyRankPanel)
        {
            Panel p = new Panel();
            p.Size = new Size(_contentWidth, 50);
            p.Margin = new Padding(0, 0, 0, 5);
            p.BackColor = Color.Transparent;

            string uName = (user.Username ?? "Unknown").ToUpper();
            string uLvl = "Lv." + (user.Info != null ? user.Info.Level : 1);
            TimeSpan ts = TimeSpan.FromHours(user.Info != null ? user.Info.TotalHours : 0);
            string uHrs = $"{(int)ts.TotalHours:00}:{ts.Minutes:00}:{ts.Seconds:00}";

            // Sự kiện Paint để vẽ nền nét đứt
            p.Paint += (s, e) =>
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle r = p.ClientRectangle;
                r.Width--; r.Height--;

                using (GraphicsPath path = GetRoundedPath(r, 20))
                {
                    // Nền trong suốt màu nâu tối (Alpha = 180)
                    using (SolidBrush b = new SolidBrush(Color.FromArgb(180, 40, 30, 20)))
                    {
                        g.FillPath(b, path);
                    }

                    // Viền nét đứt (Stitched effect)
                    using (Pen pen = new Pen(Color.Peru, 1))
                    {
                        pen.DashStyle = DashStyle.Dash;
                        g.DrawPath(pen, path);
                    }

                    // Nếu là rank của mình thì thêm viền sáng
                    if (user.Uid == _currentUser.Uid)
                    {
                        using (Pen pHigh = new Pen(Color.Gold, 2)) g.DrawPath(pHigh, path);
                    }
                }
            };

            // Rank Number
            Label lblRank = new Label() { Text = $"#{rank}", ForeColor = Color.White, Font = new Font("Segoe UI", 11, FontStyle.Bold), AutoSize = true };

            // Avatar nhỏ
            PictureBox pic = new PictureBox() { Size = new Size(30, 30), SizeMode = PictureBoxSizeMode.StretchImage, Image = GetAvatar(user.Uid, user.Username) };
            GraphicsPath gp = new GraphicsPath(); gp.AddEllipse(0, 0, 30, 30); pic.Region = new Region(gp);

            // Thông tin
            Label lblName = new Label() { Text = uName, ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold), AutoSize = true };
            Label lblLvl = new Label() { Text = uLvl, ForeColor = Color.Gray, Font = new Font("Segoe UI", 9), AutoSize = true };
            Label lblHrs = new Label() { Text = uHrs, ForeColor = Color.Gold, Font = new Font("Consolas", 11, FontStyle.Bold), AutoSize = true };

            p.Controls.AddRange(new Control[] { lblRank, pic, lblName, lblLvl, lblHrs });

            if (!isMyRankPanel) AdjustRowItems(p, true);
            return p;
        }

        // HÀM QUAN TRỌNG: TẠO AVATAR MẶC ĐỊNH & XỬ LÝ ẢNH

        private Image GetAvatar(string uid, string username)
        {
            // 1. Tìm ảnh trong thư mục UserAvatars
            string path = Path.Combine(Application.StartupPath, "UserAvatars", $"{uid}.jpg");
            if (File.Exists(path))
            {
                try
                {
                    // Load ảnh an toàn
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        return Image.FromStream(fs);
                    }
                }
                catch { }
            }

            // 2. Tìm trong Resource 
            try
            {
                // Lấy dưới dạng Object để kiểm tra kiểu
                object obj = Properties.Resources.ResourceManager.GetObject("profile");
                if (obj != null)
                {
                    // Chuyển đổi an toàn
                    Image img = ByteToImg(obj);
                    if (img != null) return img;
                }
            }
            catch { }

            // 3. Fallback: Vẽ hình tròn xám có chữ cái đầu (Fix triệt để lỗi mất ảnh)
            int size = 100;
            Bitmap bmp = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                // Vẽ nền tròn
                using (SolidBrush b = new SolidBrush(Color.DimGray))
                {
                    g.FillEllipse(b, 0, 0, size, size);
                }

                // Vẽ chữ
                string initial = string.IsNullOrEmpty(username) ? "?" : username.Substring(0, 1).ToUpper();
                using (Font f = new Font("Arial", 40, FontStyle.Bold))
                {
                    SizeF s = g.MeasureString(initial, f);
                    g.DrawString(initial, f, Brushes.White, (size - s.Width) / 2, (size - s.Height) / 2);
                }
            }
            return bmp;
        }
        //-------------
        private Image ByteToImg(object resource)
        {
            if (resource == null) return null;

            // Nếu đã là Image thì trả về luôn
            if (resource is Image img) return img;

            // Nếu là byte[] thì chuyển thành Image
            if (resource is byte[] bytes)
            {
                try
                {
                    return Image.FromStream(new MemoryStream(bytes));
                }
                catch { return null; }
            }

            return null;
        }

        // --- CÁC HÀM HỖ TRỢ UI KHÁC ---
        private void LblMenu_Click(object sender, EventArgs e) => OnMenuClicked?.Invoke();

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ResponsiveLayout();
            this.Invalidate();
        }
        //--------------------
        private void ResponsiveLayout()
        {
            if (this.Width == 0) return;
            _contentWidth = Math.Min(this.Width - 40, 1000);
            int startX = (this.Width - _contentWidth) / 2;

            pnlTabs.Location = new Point(this.Width - pnlTabs.Width - 50, 20);

            pnlPodium.Size = new Size(this.Width, 380);
            pnlPodium.Location = new Point(0, 70);
            pnlPodium.Invalidate();

            int footerHeight = 70;
            int listStartY = 480;
            int listHeight = this.Height - listStartY - footerHeight - 10;
            if (listHeight < 100) listHeight = 100;

            pnlHeaderRow.Size = new Size(_contentWidth, 25);
            pnlHeaderRow.Location = new Point(startX, listStartY - 30);
            AdjustRowItems(pnlHeaderRow, false);

            flpLeaderboard.Size = new Size(_contentWidth + 20, listHeight);
            flpLeaderboard.Location = new Point(startX, listStartY);

            pnlMyRank.Size = new Size(_contentWidth, footerHeight);
            pnlMyRank.Location = new Point(startX, this.Height - footerHeight - 5);

            foreach (Control c in flpLeaderboard.Controls) { c.Width = _contentWidth; AdjustRowItems(c as Panel, true); c.Invalidate(); }
            if (pnlMyRank.Controls.Count > 0) AdjustRowItems(pnlMyRank.Controls[0] as Panel, true);
        }

        private void AdjustRowItems(Panel p, bool isRowItem)
        {
            if (p == null) return;
            int w = p.Width;
            if (isRowItem)
            {
                if (p.Controls.Count < 5) return;
                p.Controls[0].Location = new Point(20, 15);  // Rank
                p.Controls[1].Location = new Point(60, 10);  // Avatar
                p.Controls[2].Location = new Point(110, 15); // Name
                p.Controls[3].Location = new Point(300, 15); // Level
                p.Controls[4].Location = new Point(w - 120, 15); // Time
            }
            else
            {
                lblH_Rank.Location = new Point(20, 5);
                lblH_Info.Location = new Point(110, 5);
                lblH_Lvl.Location = new Point(300, 5);
                lblH_Time.Location = new Point(w - 120, 5);
            }
        }
        //------------------------------------------------------------------
    }
}
