using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using Do_an.Models;
using Do_an.Firebase;

namespace Do_an.Forms
{
    public partial class FormScoreHistory : Form
    {
        private User _user;
        private FirebaseDatabaseService _dbService;
        private System.Windows.Forms.Timer _refreshTimer; 

        public FormScoreHistory(User user)
        {
            _user = user;
            _dbService = new FirebaseDatabaseService();
            InitializeComponent();

            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

          
            LoadDataToUI();
            ApplyCustomLayout();
            SetupStatsPanel();

            
            _ = RefreshDataFromServer();

           
            _refreshTimer = new System.Windows.Forms.Timer();
            _refreshTimer.Interval = 3000;
            _refreshTimer.Tick += async (s, e) => await RefreshDataFromServer();
            _refreshTimer.Start();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_refreshTimer != null)
            {
                _refreshTimer.Stop();
                _refreshTimer.Dispose();
            }
            base.OnFormClosing(e);
        }

        private async Task RefreshDataFromServer()
        {
            try
            {
                await _dbService.CalculateAndSaveRankAsync(_user.Uid);

               
                var latestInfo = await _dbService.GetUserProfileAsync(_user.Uid);

                if (latestInfo != null)
                {
                    _user.Info = latestInfo;

                    if (!this.IsDisposed && this.IsHandleCreated)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            SetupStatsPanel();
                            LoadDataToUI();    
                        });
                    }
                }
            }
            catch { }
        }

        private void SetupStatsPanel()
        {
            pnlStats.Controls.Clear();

            int totalWidth = pnlStats.Width;
            int gap = 20;
            int totalGap = gap * 2;
            int cardWidth = (totalWidth - totalGap) / 3;
            int cardHeight = pnlStats.Height;


            StatCard c1 = new StatCard()
            {
                Title = "Hạng tuần này",
                Value = _user.Info.WeeklyRank > 0 ? $"#{_user.Info.WeeklyRank}" : "--",
                Unit = "HẠNG",
                ColorStart = Color.FromArgb(255, 60, 0), 
                ColorEnd = Color.FromArgb(255, 100, 50),
                Size = new Size(cardWidth, cardHeight),
                Location = new Point(0, 0)
            };

            StatCard c2 = new StatCard()
            {
                Title = "Hạng cao nhất",
                Value = _user.Info.HighestRank > 0 ? $"#{_user.Info.HighestRank}" : "--",
                Unit = "CAO NHẤT",
                ColorStart = Color.FromArgb(255, 140, 0), 
                ColorEnd = Color.FromArgb(255, 180, 0),
                Size = new Size(cardWidth, cardHeight),
                Location = new Point(cardWidth + gap, 0)
            };

            StatCard c3 = new StatCard()
            {
                Title = "Lần lọt Top",
                Value = $"{_user.Info.TotalTopReach}",
                Unit = "LẦN",
                ColorStart = Color.FromArgb(0, 180, 80), 
                ColorEnd = Color.FromArgb(50, 220, 100),
                Size = new Size(cardWidth, cardHeight),
                Location = new Point((cardWidth + gap) * 2, 0)
            };

            pnlStats.Controls.AddRange(new Control[] { c1, c2, c3 });
        }


        private void ApplyCustomLayout()
        {
            lblClose.Location = new Point(this.Width - 40, 10);
            picAvatar.Location = new Point(50, 50);
            picAvatar.Size = new Size(120, 120);

            lblName.Location = new Point(190, 50);
            lblTitleHeader.Location = new Point(190, 90);
            lblLevel.Location = new Point(190, 125);
            lblXP.Location = new Point(250, 127);

            line.Size = new Size(this.Width - 100, 1);
            line.Location = new Point(50, 190);

            int startY = line.Location.Y + 30;
            int fixedHeight = 250;

            pnlStats.Location = new Point(50, startY);
            pnlStats.Size = new Size(this.Width - 100, fixedHeight);
        }

        private void LoadDataToUI()
        {
            lblName.Text = _user.Username.ToUpper();
            lblLevel.Text = $"Lv. {_user.Info.Level}";
            lblXP.Text = $"{_user.Info.XP} / {_user.Info.XPToNextLevel} XP";

            string avatarPath = Path.Combine(Application.StartupPath, "UserAvatars", $"{_user.Uid}.jpg");
            if (File.Exists(avatarPath))
            {
                try
                {
                    using (var fs = new FileStream(avatarPath, FileMode.Open, FileAccess.Read))
                        picAvatar.Image = Image.FromStream(fs);
                }
                catch { }
            }
            else
            {
                if (Properties.Resources.profile != null)
                {
                    using (MemoryStream ms = new MemoryStream(Properties.Resources.profile))
                        picAvatar.Image = Image.FromStream(ms);
                }
            }
        }

        private void LblClose_Click(object sender, EventArgs e) => this.Close();

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