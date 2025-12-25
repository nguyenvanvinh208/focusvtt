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
    }
}
