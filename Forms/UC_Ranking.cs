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
    }
}
