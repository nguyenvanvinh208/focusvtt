using Do_an.Firebase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        // Biến xử lý kéo thả form
        private Point dragStartPoint = new Point(0, 0);
        private bool isDragging = false;

        // Custom Title Bar
        private Panel pnlControlBar;
        private PictureBox btnMin, btnMax, btnClose;

        // --- PALETTE MÀU VINTAGE (Cổ điển) ---
        private readonly Color clrSidebarBg = Color.FromArgb(101, 67, 33); // Gỗ tối (Dark Walnut)
        private readonly Color clrHoverBtn = Color.FromArgb(160, 100, 40); // Gỗ sáng (Highlight)
        private readonly Color clrTextActive = Color.White;
        private readonly Color clrTextInactive = Color.NavajoWhite; // Màu kem vàng
        private readonly Color clrControlIcon = Color.FromArgb(60, 40, 20); // Icon màu nâu đen
        private readonly Color clrBackground = Color.FromArgb(253, 248, 235); // Nền kem giấy
        // -------------------------------------
        public MainDashboard(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _authService = new FirebaseAuthService();
            _dbService = new FirebaseDatabaseService();

            if (!Directory.Exists(_avatarFolder))
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, _avatarFolder));

            // Cấu hình Form
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Normal;
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            this.BackColor = clrBackground;

            AddControlBar();

            // Bắt đầu cập nhật dữ liệu và tìm IP cho tính năng Gọi điện
            _ = RefreshDataFromServer();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CustomizeSidebar();
            SetupAvatarUI();
            LoadUserData();
            StartClock();
            LoadHome(); // Mặc định load trang chủ

            // Sự kiện kéo form cho Panel Content
            pnlContent.MouseDown += PnlContent_MouseDown;
            pnlContent.MouseMove += PnlContent_MouseMove;
            pnlContent.MouseUp += PnlContent_MouseUp;

            // Sự kiện kéo form cho Sidebar (vùng trống)
            pnlSidebar.MouseDown += PnlContent_MouseDown;
            pnlSidebar.MouseMove += PnlContent_MouseMove;
            pnlSidebar.MouseUp += PnlContent_MouseUp;
        }
        #region Navigation & UserControls
        private void LoadHome()
        {
            pnlContent.Controls.Clear();
            _ucHome = new UC_Home();
            _ucHome.Dock = DockStyle.Fill;
            _ucHome.OnMenuClicked += OpenMenu;

            // Xử lý click Banner (Schedule, Ranking, Chat/Call)
            _ucHome.OnBannerClicked += (featureId) =>
            {
                switch (featureId)
                {
                    case "SCHEDULE": LoadSchedule(); break;
                    case "RANKING": LoadRanking(); break;
                    case "CHAT": LoadChat(); break;
                        // Nếu có tính năng VIDEO_CALL riêng biệt từ banner:
                        // case "VIDEO_CALL": LoadChat(); break; 
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
                // Khởi tạo Chat với thông tin user hiện tại
                _ucMessage.InitializeCurrentUser(_currentUser.Uid, _currentUser.Email);
                _ucMessage.OnMenuClicked += OpenMenu;
            }
            pnlContent.Controls.Add(_ucMessage);
            pnlSidebar.Visible = false;
        }
        // Sự kiện Click Menu Sidebar
        private void btnHome_Click(object sender, EventArgs e) { LoadHome(); pnlSidebar.Visible = false; }
        private void btnSchedule_Click(object sender, EventArgs e) { LoadSchedule(); }
        private void btnRanking_Click(object sender, EventArgs e) { LoadRanking(); }
        private void btnChat_Click(object sender, EventArgs e) { LoadChat(); }
        private void BtnCloseMenu_Click(object sender, EventArgs e) => pnlSidebar.Visible = false;

        public void OpenMenu() { pnlSidebar.BringToFront(); pnlSidebar.Visible = true; }
        #region Logic Server & Calling Support (IP Detection)
        private async Task RefreshDataFromServer()
        {
            try
            {
                // 1. Cập nhật thông tin User từ Firebase
                var latestUser = await _dbService.GetUserAsync(_currentUser.Uid);
                if (latestUser != null)
                {
                    _currentUser.Username = latestUser.Username;
                    _currentUser.Info = latestUser.Info ?? new UserProfile();

                    // Lưu avatar mới nếu có
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

                // 2. LOGIC TÌM IP (QUAN TRỌNG CHO GỌI ĐIỆN)
                // Sử dụng STUN để tìm Public IP, fallback về LAN IP nếu không có mạng
                string finalIP = "";

                // Bước 2a: Thử lấy Public IP qua STUN (Port 11000)
                await Task.Run(() =>
                {
                    try
                    {
                        var ep = Do_an.Services.StunHelper.QueryPublicEndPoint(11000);
                        if (ep != null) finalIP = $"{ep.Address}:{ep.Port}";
                    }
                    catch { }
                });

                // Bước 2b: Nếu không lấy được Public IP, tìm LAN IP
                if (string.IsNullOrEmpty(finalIP))
                {
                    foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
                    {
                        if (ni.OperationalStatus != OperationalStatus.Up) continue;
                        if (ni.NetworkInterfaceType == NetworkInterfaceType.Loopback) continue;

                        foreach (var ip in ni.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                string s = ip.Address.ToString();
                                // Bỏ qua IP rác hoặc localhost
                                if (s.StartsWith("169.254") || s.StartsWith("127.")) continue;

                                // Ưu tiên IP Radmin/VPN (26.) hoặc IP LAN (192.168.)
                                if (s.StartsWith("26.") || s.StartsWith("10."))
                                {
                                    finalIP = s + ":11000";
                                    goto FoundIP;
                                }
                                if (s.StartsWith("192.168."))
                                    finalIP = s + ":11000";
                            }
                        }
                    }
                }

            FoundIP:;
                if (string.IsNullOrEmpty(finalIP)) finalIP = "127.0.0.1:11000";

                // 3. Cập nhật UI và Database
                this.Invoke((MethodInvoker)delegate {
                    this.Text = $"FOCUS VTT - My IP: {finalIP}";
                });

                // Cập nhật vào biến cục bộ (để truyền cho Video Call nếu cần)
                _currentUser.LocalIP = finalIP;

                // Đẩy IP lên Firebase để người khác gọi được mình
                await _dbService.UpdateUserLocalIPAsync(_currentUser.Uid, finalIP);
            }
            catch { }
        }
    }
}
