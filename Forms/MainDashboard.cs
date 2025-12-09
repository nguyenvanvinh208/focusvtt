using Do_an.Firebase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
    }
}
