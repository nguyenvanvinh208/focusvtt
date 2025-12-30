using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;
using Do_an.Models;
using Do_an.Firebase;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace Do_an.Forms
{
    public partial class MainDashboard : Form
    {
        private User _currentUser;
        private UC_Home _ucHome;
        private FormSchedule _frmSchedule;
        private UC_Ranking _ucRanking;
        private UC_Message _ucMessage;
        private FirebaseAuthService _authService;
        private FirebaseDatabaseService _dbService;
        private System.Windows.Forms.Timer _clockTimer;
        private string _avatarFolder = Path.Combine(Application.StartupPath, "UserAvatars");

        private Point dragStartPoint = new Point(0, 0);
        private bool isDragging = false;
        private Panel pnlControlBar;
        private PictureBox btnMin, btnMax, btnClose;

        public MainDashboard(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _authService = new FirebaseAuthService();
            _dbService = new FirebaseDatabaseService();

            if (!Directory.Exists(_avatarFolder))
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, _avatarFolder));

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Normal;
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;

            AddControlBar();
            _ = RefreshDataFromServer();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CustomizeSidebar();
            SetupAvatarUI();
            LoadUserData();
            StartClock();
            LoadHome();

            pnlContent.MouseDown += PnlContent_MouseDown;
            pnlContent.MouseMove += PnlContent_MouseMove;
            pnlContent.MouseUp += PnlContent_MouseUp;
        }

        private void LoadHome()
        {
            pnlContent.Controls.Clear();
            _ucHome = new UC_Home();
            _ucHome.Dock = DockStyle.Fill;
            _ucHome.OnMenuClicked += OpenMenu;
            _ucHome.OnBannerClicked += (featureId) =>
            {
                switch (featureId)
                {
                    case "SCHEDULE": LoadSchedule(); break;
                    case "RANKING": LoadRanking(); break;
                    case "CHAT": LoadChat(); break;
                }
            };
            pnlContent.Controls.Add(_ucHome);
        }

        private void LoadSchedule()
        {
            pnlContent.Controls.Clear();
            if (_frmSchedule == null || _frmSchedule.IsDisposed)
            {
                _frmSchedule = new FormSchedule(_currentUser);
                _frmSchedule.TopLevel = false;
                _frmSchedule.FormBorderStyle = FormBorderStyle.None;
                _frmSchedule.Dock = DockStyle.Fill;
                _frmSchedule.OnMenuClicked += OpenMenu;
            }
            pnlContent.Controls.Add(_frmSchedule);
            _frmSchedule.Show();
            pnlSidebar.Visible = false;
        }

        private void LoadRanking()
        {
            pnlContent.Controls.Clear();
            _ucRanking = new UC_Ranking(_currentUser);
            _ucRanking.Dock = DockStyle.Fill;
            _ucRanking.OnMenuClicked += OpenMenu;
            pnlContent.Controls.Add(_ucRanking);
            pnlSidebar.Visible = false;
        }

        private void LoadChat()
        {
            pnlContent.Controls.Clear();
            if (_ucMessage == null || _ucMessage.IsDisposed)
            {
                _ucMessage = new UC_Message();
                _ucMessage.Dock = DockStyle.Fill;
                _ucMessage.InitializeCurrentUser(_currentUser.Uid, _currentUser.Email);
                _ucMessage.OnMenuClicked += OpenMenu;
            }
            pnlContent.Controls.Add(_ucMessage);
            pnlSidebar.Visible = false;
        }

        private void btnHome_Click(object sender, EventArgs e) { LoadHome(); pnlSidebar.Visible = false; }
        private void btnSchedule_Click(object sender, EventArgs e) { LoadSchedule(); }
        private void btnRanking_Click(object sender, EventArgs e) { LoadRanking(); }
        private void btnChat_Click(object sender, EventArgs e) { LoadChat(); }

        public void OpenMenu() { pnlSidebar.BringToFront(); pnlSidebar.Visible = true; }

        private async Task RefreshDataFromServer()
        {
            try
            {
                // 1. Cập nhật thông tin User
                var latestUser = await _dbService.GetUserAsync(_currentUser.Uid);
                if (latestUser != null)
                {
                    _currentUser.Username = latestUser.Username;
                    _currentUser.Info = latestUser.Info ?? new UserProfile();
                    if (!string.IsNullOrEmpty(latestUser.AvatarBase64))
                    {
                        string path = Path.Combine(_avatarFolder, $"{_currentUser.Uid}.jpg");
                        try { File.WriteAllBytes(path, Convert.FromBase64String(latestUser.AvatarBase64)); } catch { }
                    }

                    this.Invoke((MethodInvoker)delegate {
                        lblUsername.Text = _currentUser.Username;
                        LoadUserData();
                    });
                }

                // 2. Tìm IP Wifi thật (Bỏ qua máy ảo, loopback)
                string finalIP = "";
                foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (ni.NetworkInterfaceType != NetworkInterfaceType.Ethernet &&
                        ni.NetworkInterfaceType != NetworkInterfaceType.Wireless80211) continue;

                    if (ni.OperationalStatus != OperationalStatus.Up) continue;

                    string desc = ni.Description.ToLower();
                    if (desc.Contains("virtual") || desc.Contains("vmware") || desc.Contains("vpn") || desc.Contains("bluetooth") || desc.Contains("loopback"))
                        continue;

                    foreach (var ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            string ipStr = ip.Address.ToString();
                            if (!ipStr.StartsWith("169.254") && !ipStr.StartsWith("127."))
                            {
                                finalIP = ipStr;
                                if (ni.Name.ToLower().Contains("wi-fi") || ni.Name.ToLower().Contains("wireless"))
                                {
                                    goto FoundIP;
                                }
                            }
                        }
                    }
                }

            FoundIP:;

                if (string.IsNullOrEmpty(finalIP))
                    finalIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();

                // [ĐÃ XÓA MESSAGE BOX, CHỈ CẬP NHẬT TIÊU ĐỀ ÂM THẦM]
                this.Invoke((MethodInvoker)delegate {
                    this.Text = $"FOCUS VTT - LAN IP: {finalIP}";
                });

                if (!string.IsNullOrEmpty(finalIP))
                {
                    await _dbService.UpdateUserLocalIPAsync(_currentUser.Uid, finalIP);
                }
            }
            catch { }
        }

        private void BtnCloseMenu_Click(object sender, EventArgs e) => pnlSidebar.Visible = false;

        private void AddControlBar()
        {
            pnlControlBar = new Panel() { BackColor = Color.Transparent, Size = new Size(120, 30), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            btnClose = CreateControlButton(DrawCloseIcon, BtnCloseApp_Click, Color.Red);
            btnMax = CreateControlButton(DrawMaximizeIcon, BtnMaximizeApp_Click, Color.Teal);
            btnMin = CreateControlButton(DrawMinimizeIcon, BtnMinimizeApp_Click, Color.Teal);
            btnClose.Location = new Point(80, 0); btnMax.Location = new Point(40, 0); btnMin.Location = new Point(0, 0);
            pnlControlBar.Controls.AddRange(new Control[] { btnClose, btnMax, btnMin });
            this.Controls.Add(pnlControlBar); pnlControlBar.BringToFront();
        }

        public delegate void DrawIconDelegate(Graphics g, Rectangle rect, Color foreColor);
        private PictureBox CreateControlButton(DrawIconDelegate drawFunc, EventHandler clickHandler, Color hoverColor)
        {
            PictureBox btn = new PictureBox() { Size = new Size(30, 30), SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.Transparent, Cursor = Cursors.Hand, Tag = drawFunc };
            btn.Paint += (s, e) => { e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; ((DrawIconDelegate)btn.Tag)(e.Graphics, e.ClipRectangle, Color.Black); };
            btn.MouseEnter += (s, e) => { btn.BackColor = hoverColor; btn.Invalidate(); };
            btn.MouseLeave += (s, e) => { btn.BackColor = Color.Transparent; btn.Invalidate(); };
            btn.Click += clickHandler; return btn;
        }

        private void DrawCloseIcon(Graphics g, Rectangle r, Color c) { using (Pen p = new Pen(c, 2)) { g.DrawLine(p, r.X + 8, r.Y + 8, r.Right - 8, r.Bottom - 8); g.DrawLine(p, r.Right - 8, r.Y + 8, r.X + 8, r.Bottom - 8); } }
        private void DrawMaximizeIcon(Graphics g, Rectangle r, Color c) { using (Pen p = new Pen(c, 2)) { g.DrawRectangle(p, r.X + 8, r.Y + 8, r.Width - 16, r.Height - 16); } }
        private void DrawMinimizeIcon(Graphics g, Rectangle r, Color c) { using (Pen p = new Pen(c, 2)) { g.DrawLine(p, r.X + 12, r.Bottom - 12, r.Right - 12, r.Bottom - 12); } }

        private void CustomizeSidebar() { pnlSidebar.Dock = DockStyle.None; pnlSidebar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left; pnlSidebar.BackColor = Color.FromArgb(240, 25, 25, 35); pnlSidebar.Size = new Size(280, this.ClientSize.Height - 100); pnlSidebar.Location = new Point(30, 50); SetRoundedRegion(pnlSidebar, 30); StyleButton(btnHome); StyleButton(btnSchedule); StyleButton(btnRanking); StyleButton(btnChat); }
        private void SetRoundedRegion(Control c, int radius) { using (GraphicsPath p = GetRoundedPath(new Rectangle(0, 0, c.Width, c.Height), radius)) c.Region = new Region(p); }
        private GraphicsPath GetRoundedPath(Rectangle r, int d) { GraphicsPath p = new GraphicsPath(); d *= 2; p.AddArc(r.X, r.Y, d, d, 180, 90); p.AddArc(r.Right - d, r.Y, d, d, 270, 90); p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90); p.AddArc(r.X, r.Bottom - d, d, d, 90, 90); p.CloseFigure(); return p; }
        private void StyleButton(Button btn) { btn.FlatStyle = FlatStyle.Flat; btn.FlatAppearance.BorderSize = 0; btn.BackColor = Color.Transparent; btn.ForeColor = Color.WhiteSmoke; btn.Font = new Font("Segoe UI", 11); btn.TextAlign = ContentAlignment.MiddleLeft; btn.Padding = new Padding(20, 0, 0, 0); btn.Cursor = Cursors.Hand; btn.MouseEnter += (s, e) => { btn.BackColor = Color.FromArgb(50, 255, 255, 255); btn.ForeColor = Color.Cyan; }; btn.MouseLeave += (s, e) => { btn.BackColor = Color.Transparent; btn.ForeColor = Color.WhiteSmoke; }; }
        private void SetupAvatarUI() { picAvatar.SizeMode = PictureBoxSizeMode.StretchImage; GraphicsPath gp = new GraphicsPath(); gp.AddEllipse(0, 0, picAvatar.Width, picAvatar.Height); picAvatar.Region = new Region(gp); }

        private void LoadUserData()
        {
            if (_currentUser != null)
            {
                lblUsername.Text = _currentUser.Username ?? "User";
                string path = Path.Combine(_avatarFolder, $"{_currentUser.Uid}.jpg");
                if (File.Exists(path)) try { using (var s = new FileStream(path, FileMode.Open, FileAccess.Read)) picAvatar.Image = Image.FromStream(s); } catch { LoadDefaultAvatar(); }
                else LoadDefaultAvatar();
            }
        }
        private void LoadDefaultAvatar() { try { using (MemoryStream ms = new MemoryStream(Properties.Resources.profile)) picAvatar.Image = Image.FromStream(ms); } catch { picAvatar.BackColor = Color.Gray; } }
        private void StartClock() { _clockTimer = new System.Windows.Forms.Timer { Interval = 1000 }; _clockTimer.Tick += (s, e) => { this.Text = $"FOCUS VTT - LAN IP: {this.Text.Split(':')[1].Trim()}"; }; _clockTimer.Start(); }
        private void BtnCloseApp_Click(object sender, EventArgs e) => Application.Exit();
        private void BtnMaximizeApp_Click(object sender, EventArgs e) { if (WindowState == FormWindowState.Normal) WindowState = FormWindowState.Maximized; else WindowState = FormWindowState.Normal; btnMax.Invalidate(); }
        private void BtnMinimizeApp_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;
        private void PnlContent_MouseDown(object sender, MouseEventArgs e) { if (e.Button == MouseButtons.Left) { dragStartPoint = new Point(e.X, e.Y); isDragging = true; } }
        private void PnlContent_MouseMove(object sender, MouseEventArgs e) { if (isDragging) { Point p1 = new Point(e.X, e.Y); Point p2 = PointToScreen(p1); Location = new Point(p2.X - dragStartPoint.X, p2.Y - dragStartPoint.Y); } }
        private void PnlContent_MouseUp(object sender, MouseEventArgs e) => isDragging = false;

        private async void BtnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Properties.Settings.Default.IsLoggedIn = false;
                Properties.Settings.Default.Save();
                if (!string.IsNullOrEmpty(_currentUser.Uid)) await _authService.LogoutAsync(_currentUser.Uid);
                Hide(); new Login().Show();
            }
        }

        private async void BtnChooseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "Image|*.jpg;*.png;*.jpeg";
            if (o.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    byte[] imageBytes;
                    using (Image img = Image.FromFile(o.FileName))
                    {
                        using (Bitmap b = new Bitmap(img, new Size(200, 200)))
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                b.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                imageBytes = ms.ToArray();
                            }
                        }
                    }
                    string base64 = Convert.ToBase64String(imageBytes);
                    await _dbService.UpdateUserAvatarAsync(_currentUser.Uid, base64);
                    string dest = Path.Combine(_avatarFolder, $"{_currentUser.Uid}.jpg");
                    File.WriteAllBytes(dest, imageBytes);
                    using (MemoryStream ms = new MemoryStream(imageBytes)) picAvatar.Image = Image.FromStream(ms);
                    MessageBox.Show("Đã cập nhật Avatar thành công!");
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private void PicAvatar_Click(object sender, EventArgs e)
        {
            ContextMenuStrip m = new ContextMenuStrip();
            m.Items.Add("Đổi Avatar", null, BtnChooseImage_Click);
            m.Items.Add("Thông tin cá nhân", null, (s, ev) => { using (Form f = new FormProfile(_currentUser)) f.ShowDialog(); });
            m.Items.Add("Lịch sử điểm", null, (s, ev) => { using (Form f = new FormScoreHistory(_currentUser)) f.ShowDialog(); });
            m.Items.Add(new ToolStripSeparator());
            m.Items.Add("Đăng xuất", null, BtnLogout_Click);
            m.Show(Cursor.Position);
        }

        protected override void OnSizeChanged(EventArgs e) { base.OnSizeChanged(e); CustomizeSidebar(); if (pnlControlBar != null) pnlControlBar.Location = new Point(ClientSize.Width - 130, 5); }
    }
}



