using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using Do_an.Models;
using Do_an.Firebase;
using Do_an.Services;
using WMPLib;
using Microsoft.Web.WebView2.Core;

namespace Do_an.Forms
{
    public partial class FormFocusMode : Form
    {
        // --- DỮ LIỆU ---
        private TaskInfo _currentTask;
        private User _currentUser;
        private DateTime _sessionStartTime;
        private DateTime _scheduledEndTime;
        private System.Windows.Forms.Timer _timerLogic;

        private ThemeService _themeService;
        private FirebaseDatabaseService _dbService;

        private List<ThemeInfo> _allThemes = new List<ThemeInfo>();
        private FlowLayoutPanel _flpThemeGrid;
        private FlowLayoutPanel _flpCategories;
        private bool _isLiveMode = false;
        private string _currentCategory = "All";
        private List<WindowsMediaPlayer> _soundPlayers = new List<WindowsMediaPlayer>();

        // --- LINK SPOTIFY ---
        private const string LINK_LOFI = "https://open.spotify.com/embed/playlist/0vvXsWCC9xrXsKd4FyS8kM?utm_source=generator&theme=0";
        private const string LINK_CAFE = "https://open.spotify.com/embed/playlist/37i9dQZF1DX6VdMW310YC7?utm_source=generator&theme=0";
        private const string LINK_SONTUNG = "https://open.spotify.com/embed/artist/5dfZ5uSmzR7VQK0udbAVpf?utm_source=generator&theme=0";
        private const string LINK_DJ = "https://open.spotify.com/embed/artist/65IEN7G4gAE2rmrhxKpQET?utm_source=generator&theme=0";

        public FormFocusMode(TaskInfo task, User user)
        {
            InitializeComponent();

            // Cấu hình Video nền
            if (wmpBackground != null)
            {
                try
                {
                    wmpBackground.uiMode = "none";
                    wmpBackground.stretchToFit = true;
                    wmpBackground.settings.autoStart = true;
                    wmpBackground.settings.volume = 0;
                    wmpBackground.enableContextMenu = false;
                    wmpBackground.SendToBack();
                }
                catch { }
            }

            // KHỞI TẠO MIXER
            InitializeSoundMixer();

            _currentTask = task;
            _currentUser = user;
            _themeService = new ThemeService();
            _dbService = new FirebaseDatabaseService();

            _sessionStartTime = DateTime.Now;
            _scheduledEndTime = task.End;

            SetupThemeUI();
            InitializeSpotify();

            _timerLogic = new System.Windows.Forms.Timer();
            _timerLogic.Interval = 1000;
            _timerLogic.Tick += TimerLogic_Tick;
            _timerLogic.Start();
        }

        // --- [ĐÃ SỬA] LOGIC TÍNH ĐIỂM & ĐỒNG BỘ ---
        private async System.Threading.Tasks.Task CalculateAndSaveScore(TimeSpan duration)
        {
            // 1. Tính toán
            int minutes = (int)duration.TotalMinutes;
            if (minutes < 1) minutes = 1; // Tối thiểu 1 phút

            int xpEarned = minutes * 10;
            double hoursEarned = duration.TotalHours;

            // 2. Cập nhật ngay vào biến User trên máy (Local)
            _currentUser.Info.XP += xpEarned;
            _currentUser.Info.TotalHours += hoursEarned;

            // Logic lên cấp local
            while (_currentUser.Info.XP >= _currentUser.Info.XPToNextLevel)
            {
                _currentUser.Info.XP -= _currentUser.Info.XPToNextLevel;
                _currentUser.Info.Level++;
                _currentUser.Info.XPToNextLevel += 50;
                MessageBox.Show($"CHÚC MỪNG! BẠN ĐÃ LÊN LEVEL {_currentUser.Info.Level}!");
            }

            try
            {
                // 3. Gửi lên Server lưu trữ
                await _dbService.AddXpAndLevelUpAsync(_currentUser.Uid, xpEarned, hoursEarned);

                // [MỚI] Hiển thị thông báo thời gian dạng HH:mm:ss
                string timeStr = $"{(int)duration.TotalHours:00}:{duration.Minutes:00}:{duration.Seconds:00}";

                MessageBox.Show($"Hoàn thành phiên làm việc!\n\n+{xpEarned} XP\nThời gian học: {timeStr}",
                                "Tổng kết", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đồng bộ điểm lên Server: " + ex.Message);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // --- CÁC HÀM KHÁC GIỮ NGUYÊN ---
        private void InitializeSoundMixer()
        {
            string baseUrl = "https://raw.githubusercontent.com/nguyenvanvinh208/FocusVTT-Data/main/";
            AddSoundControl("🌧️ Rain", baseUrl + "light-rain-109591.mp3");
            AddSoundControl("⚡ Thunder", baseUrl + "thunder-sound-375727.mp3");
            AddSoundControl("🌲 Forest", baseUrl + "forest-morning-28889.mp3");
            AddSoundControl("💧 Creek", baseUrl + "soothing-creek-420908.mp3");
            AddSoundControl("🐓 Morning", baseUrl + "roosters-early-morning-32050.mp3");
        }

        private void AddSoundControl(string name, string url)
        {
            WindowsMediaPlayer player = new WindowsMediaPlayer();
            player.settings.autoStart = false;
            player.settings.setMode("loop", true);
            player.URL = url;
            player.settings.volume = 0;
            _soundPlayers.Add(player);

            Panel pnlItem = new Panel();
            pnlItem.Size = new Size(300, 60);
            pnlItem.BackColor = Color.Transparent;

            Label lblName = new Label();
            lblName.Text = name;
            lblName.ForeColor = Color.DimGray;
            lblName.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblName.Location = new Point(10, 5);
            lblName.AutoSize = true;

            TrackBar trkVol = new TrackBar();
            trkVol.Location = new Point(10, 25);
            trkVol.Size = new Size(280, 45);
            trkVol.Maximum = 100;
            trkVol.TickStyle = TickStyle.None;

            trkVol.Scroll += (s, e) =>
            {
                int vol = trkVol.Value;
                player.settings.volume = vol;
                if (vol > 0 && player.playState != WMPPlayState.wmppsPlaying)
                    player.controls.play();
                else if (vol == 0)
                    player.controls.pause();
            };

            pnlItem.Controls.Add(lblName);
            pnlItem.Controls.Add(trkVol);
            flpMixer.Controls.Add(pnlItem);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            foreach (var p in _soundPlayers) p.close();
            if (wmpBackground != null) wmpBackground.close();
            if (webViewSpotify != null) webViewSpotify.Dispose();
        }

        private async void InitializeSpotify()
        {
            try { await webViewSpotify.EnsureCoreWebView2Async(null); LoadSpotifyUrl(LINK_LOFI); } catch { }
            btnMusicLofi.Click += (s, e) => { HighlightMusicButton(btnMusicLofi); LoadSpotifyUrl(LINK_LOFI); };
            btnMusicCafe.Click += (s, e) => { HighlightMusicButton(btnMusicCafe); LoadSpotifyUrl(LINK_CAFE); };
            btnMusicMTP.Click += (s, e) => { HighlightMusicButton(btnMusicMTP); LoadSpotifyUrl(LINK_SONTUNG); };
            btnMusicDJ.Click += (s, e) => { HighlightMusicButton(btnMusicDJ); LoadSpotifyUrl(LINK_DJ); };
            HighlightMusicButton(btnMusicLofi);
        }

        private void LoadSpotifyUrl(string url) { if (webViewSpotify != null && webViewSpotify.CoreWebView2 != null) webViewSpotify.CoreWebView2.Navigate(url); }
        private void HighlightMusicButton(Button selectedBtn) { StyleMusicButton(btnMusicLofi, false); StyleMusicButton(btnMusicCafe, false); StyleMusicButton(btnMusicMTP, false); StyleMusicButton(btnMusicDJ, false); StyleMusicButton(selectedBtn, true); }
        private void StyleMusicButton(Button btn, bool isActive) { if (isActive) { btn.BackColor = Color.FromArgb(29, 185, 84); btn.ForeColor = Color.White; } else { btn.BackColor = Color.FromArgb(60, 60, 70); btn.ForeColor = Color.Silver; } }
        private void ShowPanelAboveButton(Panel pnl, Button btn) { if (pnl != pnlScenes) pnlScenes.Visible = false; if (pnl != pnlSounds) pnlSounds.Visible = false; if (pnl != pnlSpotify) pnlSpotify.Visible = false; if (pnl.Visible) { pnl.Visible = false; UpdateDockButtonColors(); return; } int btnX_OnForm = pnlBottomDock.Location.X + btn.Location.X; int btnY_OnForm = pnlBottomDock.Location.Y + btn.Location.Y; int finalX = (btnX_OnForm + (btn.Width / 2)) - (pnl.Width / 2); int finalY = btnY_OnForm - pnl.Height - 15; pnl.Location = new Point(finalX, finalY); pnl.Visible = true; pnl.BringToFront(); UpdateDockButtonColors(); }
        private void PnlSpotify_Paint(object sender, PaintEventArgs e) { Graphics g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias; Panel pnl = sender as Panel; Rectangle r = pnl.ClientRectangle; r.Width--; r.Height--; using (GraphicsPath path = GetRoundedPath(r, 30)) { pnl.Region = new Region(path); using (LinearGradientBrush brush = new LinearGradientBrush(r, Color.FromArgb(230, 30, 30, 30), Color.FromArgb(230, 10, 10, 10), 90F)) g.FillPath(brush, path); using (Pen pen = new Pen(Color.FromArgb(100, 29, 185, 84), 1)) g.DrawPath(pen, path); } }

        private void FormFocusMode_Load(object sender, EventArgs e)
        {
            CenterControls();
            pnlScenes.Location = new Point((this.Width - pnlScenes.Width) / 2, (this.Height - pnlScenes.Height) / 2);
            pnlSpotify.Location = new Point((this.Width - pnlSpotify.Width) / 2, (this.Height - pnlSpotify.Height) / 2);
            pnlSounds.Location = new Point(this.Width - pnlSounds.Width - 50, this.Height - 300);
            pnlBottomDock.Location = new Point((this.Width - pnlBottomDock.Width) / 2, this.Height - 100);
            SetupDockButton(btnScenes, "🖼️ Themes", 0, BtnScenes_Click);
            SetupDockButton(btnMusic, "🎵 Music", 1, BtnMusic_Click);
            SetupDockButton(btnSounds, "🔊 Sounds", 2, BtnSounds_Click);
            SetupDockButton(btnToggleTimer, "⏱️ Timer", 3, BtnToggleTimer_Click);
            SetupDockButton(btnFinish, "⏹️ End", 4, BtnFinishEarly_Click);
        }

        private void BtnScenes_Click(object sender, EventArgs e) { ShowPanelAboveButton(pnlScenes, btnScenes); }
        private void BtnSounds_Click(object sender, EventArgs e) { ShowPanelAboveButton(pnlSounds, btnSounds); }
        private void BtnMusic_Click(object sender, EventArgs e) { ShowPanelAboveButton(pnlSpotify, btnMusic); }
        private void UpdateDockButtonColors() { btnScenes.ForeColor = Color.FromArgb(64, 64, 64); btnSounds.ForeColor = Color.FromArgb(64, 64, 64); btnMusic.ForeColor = Color.FromArgb(64, 64, 64); if (pnlScenes.Visible) btnScenes.ForeColor = Color.OrangeRed; if (pnlSounds.Visible) btnSounds.ForeColor = Color.OrangeRed; if (pnlSpotify.Visible) btnMusic.ForeColor = Color.FromArgb(29, 185, 84); }
        private void BtnToggleTimer_Click(object sender, EventArgs e) { lblTimer.Visible = !lblTimer.Visible; if (lblTimer.Visible) btnToggleTimer.ForeColor = Color.FromArgb(64, 64, 64); else btnToggleTimer.ForeColor = Color.OrangeRed; }

        // [QUAN TRỌNG] HÀM CĂN CHỈNH VỊ TRÍ
        private void CenterControls()
        {
            // 1. Căn giữa Đồng hồ
            if (lblTimer != null)
                lblTimer.Location = new Point((this.Width - lblTimer.Width) / 2, (this.Height - lblTimer.Height) / 2);

            // 2. [ĐÃ SỬA] Đẩy nút thu nhỏ sát góc phải
            // this.ClientSize.Width lấy chiều rộng thực tế khi Form đang chạy (Maximized)
            if (lblMin != null)
            {
                lblMin.Location = new Point(this.ClientSize.Width - 60, 0);
                lblMin.BringToFront(); // Đảm bảo nút nằm trên cùng
            }
        }

        private async void TimerLogic_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            TimeSpan remaining = _scheduledEndTime - now;
            TimeSpan elapsed = now - _sessionStartTime;

            lblTimer.Text = remaining.TotalSeconds > 0 ? $"{remaining.Hours:00}:{remaining.Minutes:00}:{remaining.Seconds:00}" : "00:00:00";

            // Gọi hàm căn chỉnh liên tục để đảm bảo vị trí đúng
            CenterControls();

            if (now >= _scheduledEndTime)
            {
                _timerLogic.Stop();
                await CalculateAndSaveScore(elapsed);
            }
        }

        private void LblMin_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;

        private async void BtnFinishEarly_Click(object sender, EventArgs e)
        {
            _timerLogic.Stop();
            TimeSpan elapsed = DateTime.Now - _sessionStartTime;
            if (MessageBox.Show("Kết thúc sớm?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                await CalculateAndSaveScore(elapsed);
            else
                _timerLogic.Start();
        }

        private void PnlCommon_Paint(object sender, PaintEventArgs e) { Graphics g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias; Panel pnl = sender as Panel; Rectangle r = pnl.ClientRectangle; r.Width--; r.Height--; using (GraphicsPath path = GetRoundedPath(r, 40)) { pnl.Region = new Region(path); using (SolidBrush brush = new SolidBrush(Color.FromArgb(230, 255, 255, 255))) { g.FillPath(brush, path); } using (Pen pen = new Pen(Color.FromArgb(100, 200, 200, 200), 1)) { g.DrawPath(pen, path); } } }
        private async void SetupThemeUI() { Label lblLoading = new Label() { Text = "Đang tải Theme...", ForeColor = Color.DimGray, AutoSize = true, Location = new Point(20, 200), Font = new Font("Segoe UI", 12) }; pnlScenes.Controls.Add(lblLoading); lblLoading.BringToFront(); btnTabStatic.Click += (s, e) => SwitchTab(false); btnTabLive.Click += (s, e) => SwitchTab(true); _flpCategories = new FlowLayoutPanel(); _flpCategories.Size = new Size(760, 40); _flpCategories.Location = new Point(20, 70); _flpCategories.BackColor = Color.Transparent; pnlScenes.Controls.Add(_flpCategories); AddCategoryButton("All"); AddCategoryButton("Nature"); AddCategoryButton("Anime"); AddCategoryButton("Chill"); AddCategoryButton("Pets"); _flpThemeGrid = new FlowLayoutPanel(); _flpThemeGrid.Size = new Size(760, 350); _flpThemeGrid.Location = new Point(10, 120); _flpThemeGrid.AutoScroll = true; _flpThemeGrid.BackColor = Color.Transparent; pnlScenes.Controls.Add(_flpThemeGrid); _allThemes = await _themeService.GetThemesAsync(); pnlScenes.Controls.Remove(lblLoading); SwitchTab(false); }
        private void SwitchTab(bool isLive) { _isLiveMode = isLive; if (isLive) { btnTabLive.ForeColor = Color.OrangeRed; btnTabStatic.ForeColor = Color.DimGray; } else { btnTabStatic.ForeColor = Color.OrangeRed; btnTabLive.ForeColor = Color.DimGray; } FilterThemes(_currentCategory); }
        private void FilterThemes(string category) { _currentCategory = category; _flpThemeGrid.Controls.Clear(); bool hasTheme = false; foreach (var theme in _allThemes) { bool matchCat = (category == "All" || theme.Category == category); bool matchType = _isLiveMode ? !string.IsNullOrEmpty(theme.VideoUrl) : string.IsNullOrEmpty(theme.VideoUrl); if (matchCat && matchType) { ThemeCard card = new ThemeCard(theme); card.OnSelectTheme += ChangeBackground; _flpThemeGrid.Controls.Add(card); hasTheme = true; } } if (!hasTheme) { Label lblEmpty = new Label() { Text = "Chưa có theme mục này", ForeColor = Color.DimGray, AutoSize = true, Padding = new Padding(20), Font = new Font("Segoe UI", 10, FontStyle.Italic) }; _flpThemeGrid.Controls.Add(lblEmpty); } }
        private void AddCategoryButton(string text) { Button btn = new Button(); btn.Text = text; btn.Size = new Size(100, 35); btn.FlatStyle = FlatStyle.Flat; btn.FlatAppearance.BorderSize = 1; btn.FlatAppearance.BorderColor = Color.Silver; btn.BackColor = Color.WhiteSmoke; btn.ForeColor = Color.Black; btn.Font = new Font("Segoe UI", 9); btn.Cursor = Cursors.Hand; btn.Click += (s, e) => { FilterThemes(text); foreach (Control c in _flpCategories.Controls) { c.BackColor = Color.WhiteSmoke; c.ForeColor = Color.Black; } btn.BackColor = Color.Orange; btn.ForeColor = Color.White; }; _flpCategories.Controls.Add(btn); }
        private void ChangeBackground(ThemeInfo theme) { if (!string.IsNullOrEmpty(theme.VideoUrl) && wmpBackground != null) { try { wmpBackground.Visible = true; wmpBackground.URL = theme.VideoUrl; wmpBackground.Ctlcontrols.play(); wmpBackground.settings.setMode("loop", true); wmpBackground.SendToBack(); this.BackgroundImage = null; } catch { } } else if (theme.ThumbnailImg != null) { if (wmpBackground != null) { wmpBackground.Ctlcontrols.stop(); wmpBackground.Visible = false; } this.BackgroundImage = theme.ThumbnailImg; } }
        private void SetupDockButton(Button btn, string text, int index, EventHandler onClick) { int btnWidth = 100; int gap = 20; int dockW = 700; int startX = (dockW - (5 * btnWidth + 4 * gap)) / 2; btn.Text = text; btn.Size = new Size(btnWidth, 60); btn.Location = new Point(startX + index * (btnWidth + gap), 10); btn.FlatStyle = FlatStyle.Flat; btn.FlatAppearance.BorderSize = 0; btn.BackColor = Color.Transparent; btn.ForeColor = Color.FromArgb(64, 64, 64); btn.Cursor = Cursors.Hand; btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold); btn.Click += onClick; btn.MouseEnter += (s, e) => btn.ForeColor = Color.OrangeRed; btn.MouseLeave += (s, e) => btn.ForeColor = Color.FromArgb(64, 64, 64); }
        private GraphicsPath GetRoundedPath(Rectangle rect, int radius) { GraphicsPath path = new GraphicsPath(); float d = radius * 2F; path.AddArc(rect.X, rect.Y, d, d, 180, 90); path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90); path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90); path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90); path.CloseFigure(); return path; }
    }
}