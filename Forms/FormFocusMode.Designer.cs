namespace Do_an.Forms
{
    partial class FormFocusMode
    {
        private System.ComponentModel.IContainer components = null;

        // --- CONTROL ---
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.Label lblTimer;

        // --- DOCK ---
        private System.Windows.Forms.Panel pnlBottomDock;
        private System.Windows.Forms.Button btnScenes;
        private System.Windows.Forms.Button btnMusic;
        private System.Windows.Forms.Button btnSounds;
        private System.Windows.Forms.Button btnToggleTimer;
        private System.Windows.Forms.Button btnFinish;

        // --- THEMES ---
        private System.Windows.Forms.Panel pnlScenes;
        private System.Windows.Forms.Label lblTitleScenes;
        private System.Windows.Forms.Panel pnlTabs;
        private System.Windows.Forms.Button btnTabStatic;
        private System.Windows.Forms.Button btnTabLive;

        // --- SOUNDS MIXER ---
        private System.Windows.Forms.Panel pnlSounds;
        private System.Windows.Forms.Label lblTitleSounds;
        private System.Windows.Forms.FlowLayoutPanel flpMixer;

        // --- SPOTIFY ---
        private System.Windows.Forms.Panel pnlSpotify;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewSpotify;
        private System.Windows.Forms.FlowLayoutPanel flpMusicMenu;
        private System.Windows.Forms.Button btnMusicLofi;
        private System.Windows.Forms.Button btnMusicCafe;
        private System.Windows.Forms.Button btnMusicMTP;
        private System.Windows.Forms.Button btnMusicDJ;

        private AxWMPLib.AxWindowsMediaPlayer wmpBackground;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFocusMode));
            lblMin = new Label();
            lblTimer = new Label();
            pnlBottomDock = new Panel();
            btnScenes = new Button();
            btnMusic = new Button();
            btnSounds = new Button();
            btnToggleTimer = new Button();
            btnFinish = new Button();
            pnlScenes = new Panel();
            pnlTabs = new Panel();
            btnTabStatic = new Button();
            btnTabLive = new Button();
            lblTitleScenes = new Label();
            pnlSounds = new Panel();
            flpMixer = new FlowLayoutPanel();
            lblTitleSounds = new Label();
            wmpBackground = new AxWMPLib.AxWindowsMediaPlayer();
            pnlSpotify = new Panel();
            webViewSpotify = new Microsoft.Web.WebView2.WinForms.WebView2();
            flpMusicMenu = new FlowLayoutPanel();
            btnMusicLofi = new Button();
            btnMusicCafe = new Button();
            btnMusicMTP = new Button();
            btnMusicDJ = new Button();
            pnlBottomDock.SuspendLayout();
            pnlScenes.SuspendLayout();
            pnlTabs.SuspendLayout();
            pnlSounds.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)wmpBackground).BeginInit();
            pnlSpotify.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webViewSpotify).BeginInit();
            flpMusicMenu.SuspendLayout();
            SuspendLayout();
            // 
            // lblMin
            // 
            lblMin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblMin.AutoSize = true;
            lblMin.BackColor = Color.Transparent;
            lblMin.Cursor = Cursors.Hand;
            lblMin.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblMin.ForeColor = Color.White;
            lblMin.Location = new Point(1165, 0);
            lblMin.Name = "lblMin";
            lblMin.Size = new Size(34, 46);
            lblMin.TabIndex = 10;
            lblMin.Text = "_";
            lblMin.Click += LblMin_Click;
            // 
            // lblTimer
            // 
            lblTimer.AutoSize = true;
            lblTimer.BackColor = Color.Transparent;
            lblTimer.Font = new Font("Segoe UI", 72F, FontStyle.Bold);
            lblTimer.ForeColor = Color.White;
            lblTimer.Location = new Point(300, 200);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(547, 159);
            lblTimer.TabIndex = 1;
            lblTimer.Text = "00:00:00";
            lblTimer.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlBottomDock
            // 
            pnlBottomDock.BackColor = Color.Transparent;
            pnlBottomDock.Controls.Add(btnScenes);
            pnlBottomDock.Controls.Add(btnMusic);
            pnlBottomDock.Controls.Add(btnSounds);
            pnlBottomDock.Controls.Add(btnToggleTimer);
            pnlBottomDock.Controls.Add(btnFinish);
            pnlBottomDock.Location = new Point(250, 600);
            pnlBottomDock.Name = "pnlBottomDock";
            pnlBottomDock.Size = new Size(700, 80);
            pnlBottomDock.TabIndex = 5;
            pnlBottomDock.Paint += PnlCommon_Paint;
            // 
            // btnScenes
            // 
            btnScenes.Location = new Point(0, 0);
            btnScenes.Name = "btnScenes";
            btnScenes.Size = new Size(100, 60);
            btnScenes.TabIndex = 0;
            // 
            // btnMusic
            // 
            btnMusic.Location = new Point(0, 0);
            btnMusic.Name = "btnMusic";
            btnMusic.Size = new Size(100, 60);
            btnMusic.TabIndex = 1;
            // 
            // btnSounds
            // 
            btnSounds.Location = new Point(0, 0);
            btnSounds.Name = "btnSounds";
            btnSounds.Size = new Size(100, 60);
            btnSounds.TabIndex = 2;
            // 
            // btnToggleTimer
            // 
            btnToggleTimer.Location = new Point(0, 0);
            btnToggleTimer.Name = "btnToggleTimer";
            btnToggleTimer.Size = new Size(100, 60);
            btnToggleTimer.TabIndex = 3;
            // 
            // btnFinish
            // 
            btnFinish.Location = new Point(0, 0);
            btnFinish.Name = "btnFinish";
            btnFinish.Size = new Size(100, 60);
            btnFinish.TabIndex = 4;
            // 
            // pnlScenes
            // 
            pnlScenes.BackColor = Color.Transparent;
            pnlScenes.Controls.Add(pnlTabs);
            pnlScenes.Controls.Add(lblTitleScenes);
            pnlScenes.Location = new Point(100, 100);
            pnlScenes.Name = "pnlScenes";
            pnlScenes.Size = new Size(800, 500);
            pnlScenes.TabIndex = 6;
            pnlScenes.Visible = false;
            pnlScenes.Paint += PnlCommon_Paint;
            // 
            // pnlTabs
            // 
            pnlTabs.BackColor = Color.Transparent;
            pnlTabs.Controls.Add(btnTabStatic);
            pnlTabs.Controls.Add(btnTabLive);
            pnlTabs.Location = new Point(300, 15);
            pnlTabs.Name = "pnlTabs";
            pnlTabs.Size = new Size(300, 40);
            pnlTabs.TabIndex = 1;
            // 
            // btnTabStatic
            // 
            btnTabStatic.Dock = DockStyle.Left;
            btnTabStatic.FlatAppearance.BorderSize = 0;
            btnTabStatic.FlatStyle = FlatStyle.Flat;
            btnTabStatic.Location = new Point(0, 0);
            btnTabStatic.Name = "btnTabStatic";
            btnTabStatic.Size = new Size(140, 40);
            btnTabStatic.TabIndex = 0;
            btnTabStatic.Text = "🖼️ Static Themes";
            // 
            // btnTabLive
            // 
            btnTabLive.Dock = DockStyle.Right;
            btnTabLive.FlatAppearance.BorderSize = 0;
            btnTabLive.FlatStyle = FlatStyle.Flat;
            btnTabLive.Location = new Point(160, 0);
            btnTabLive.Name = "btnTabLive";
            btnTabLive.Size = new Size(140, 40);
            btnTabLive.TabIndex = 1;
            btnTabLive.Text = "▶️ Live Themes";
            // 
            // lblTitleScenes
            // 
            lblTitleScenes.Dock = DockStyle.Top;
            lblTitleScenes.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitleScenes.ForeColor = Color.FromArgb(64, 64, 64);
            lblTitleScenes.Location = new Point(0, 0);
            lblTitleScenes.Name = "lblTitleScenes";
            lblTitleScenes.Padding = new Padding(20, 10, 0, 0);
            lblTitleScenes.Size = new Size(800, 50);
            lblTitleScenes.TabIndex = 0;
            lblTitleScenes.Text = "Themes Collection";
            // 
            // pnlSounds
            // 
            pnlSounds.BackColor = Color.Transparent;
            pnlSounds.Controls.Add(flpMixer);
            pnlSounds.Controls.Add(lblTitleSounds);
            pnlSounds.Location = new Point(400, 100);
            pnlSounds.Name = "pnlSounds";
            pnlSounds.Size = new Size(350, 350);
            pnlSounds.TabIndex = 7;
            pnlSounds.Visible = false;
            pnlSounds.Paint += PnlCommon_Paint;
            // 
            // flpMixer
            // 
            flpMixer.AutoScroll = true;
            flpMixer.BackColor = Color.Transparent;
            flpMixer.Dock = DockStyle.Fill;
            flpMixer.Location = new Point(0, 40);
            flpMixer.Name = "flpMixer";
            flpMixer.Padding = new Padding(20, 10, 0, 0);
            flpMixer.Size = new Size(350, 310);
            flpMixer.TabIndex = 0;
            // 
            // lblTitleSounds
            // 
            lblTitleSounds.Dock = DockStyle.Top;
            lblTitleSounds.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitleSounds.ForeColor = Color.FromArgb(64, 64, 64);
            lblTitleSounds.Location = new Point(0, 0);
            lblTitleSounds.Name = "lblTitleSounds";
            lblTitleSounds.Size = new Size(350, 40);
            lblTitleSounds.TabIndex = 0;
            lblTitleSounds.Text = "Nature Mixer";
            lblTitleSounds.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // wmpBackground
            // 
            wmpBackground.Dock = DockStyle.Fill;
            wmpBackground.Enabled = true;
            wmpBackground.Location = new Point(0, 0);
            wmpBackground.Name = "wmpBackground";
            wmpBackground.OcxState = (AxHost.State)resources.GetObject("wmpBackground.OcxState");
            wmpBackground.Size = new Size(1200, 800);
            wmpBackground.TabIndex = 0;
            // 
            // pnlSpotify
            // 
            pnlSpotify.BackColor = Color.Transparent;
            pnlSpotify.Controls.Add(webViewSpotify);
            pnlSpotify.Controls.Add(flpMusicMenu);
            pnlSpotify.Location = new Point(600, 100);
            pnlSpotify.Name = "pnlSpotify";
            pnlSpotify.Size = new Size(580, 400);
            pnlSpotify.TabIndex = 8;
            pnlSpotify.Visible = false;
            pnlSpotify.Paint += PnlSpotify_Paint;
            // 
            // webViewSpotify
            // 
            webViewSpotify.AllowExternalDrop = true;
            webViewSpotify.CreationProperties = null;
            webViewSpotify.DefaultBackgroundColor = Color.FromArgb(30, 30, 30);
            webViewSpotify.Dock = DockStyle.Fill;
            webViewSpotify.Location = new Point(0, 60);
            webViewSpotify.Name = "webViewSpotify";
            webViewSpotify.Size = new Size(580, 340);
            webViewSpotify.TabIndex = 1;
            webViewSpotify.ZoomFactor = 0.9D;
            // 
            // flpMusicMenu
            // 
            flpMusicMenu.BackColor = Color.Transparent;
            flpMusicMenu.Controls.Add(btnMusicLofi);
            flpMusicMenu.Controls.Add(btnMusicCafe);
            flpMusicMenu.Controls.Add(btnMusicMTP);
            flpMusicMenu.Controls.Add(btnMusicDJ);
            flpMusicMenu.Dock = DockStyle.Top;
            flpMusicMenu.Location = new Point(0, 0);
            flpMusicMenu.Name = "flpMusicMenu";
            flpMusicMenu.Padding = new Padding(30, 12, 0, 0);
            flpMusicMenu.Size = new Size(580, 60);
            flpMusicMenu.TabIndex = 2;
            // 
            // btnMusicLofi
            // 
            btnMusicLofi.Cursor = Cursors.Hand;
            btnMusicLofi.FlatAppearance.BorderSize = 0;
            btnMusicLofi.FlatStyle = FlatStyle.Flat;
            btnMusicLofi.Location = new Point(33, 15);
            btnMusicLofi.Name = "btnMusicLofi";
            btnMusicLofi.Size = new Size(100, 35);
            btnMusicLofi.TabIndex = 0;
            btnMusicLofi.Text = "🎧 Lofi";
            // 
            // btnMusicCafe
            // 
            btnMusicCafe.Cursor = Cursors.Hand;
            btnMusicCafe.FlatAppearance.BorderSize = 0;
            btnMusicCafe.FlatStyle = FlatStyle.Flat;
            btnMusicCafe.Location = new Point(146, 15);
            btnMusicCafe.Margin = new Padding(10, 3, 3, 3);
            btnMusicCafe.Name = "btnMusicCafe";
            btnMusicCafe.Size = new Size(100, 35);
            btnMusicCafe.TabIndex = 1;
            btnMusicCafe.Text = "☕ Cafe";
            // 
            // btnMusicMTP
            // 
            btnMusicMTP.Cursor = Cursors.Hand;
            btnMusicMTP.FlatAppearance.BorderSize = 0;
            btnMusicMTP.FlatStyle = FlatStyle.Flat;
            btnMusicMTP.Location = new Point(259, 15);
            btnMusicMTP.Margin = new Padding(10, 3, 3, 3);
            btnMusicMTP.Name = "btnMusicMTP";
            btnMusicMTP.Size = new Size(110, 35);
            btnMusicMTP.TabIndex = 2;
            btnMusicMTP.Text = "🎤 Sơn Tùng";
            // 
            // btnMusicDJ
            // 
            btnMusicDJ.Cursor = Cursors.Hand;
            btnMusicDJ.FlatAppearance.BorderSize = 0;
            btnMusicDJ.FlatStyle = FlatStyle.Flat;
            btnMusicDJ.Location = new Point(382, 15);
            btnMusicDJ.Margin = new Padding(10, 3, 3, 3);
            btnMusicDJ.Name = "btnMusicDJ";
            btnMusicDJ.Size = new Size(100, 35);
            btnMusicDJ.TabIndex = 3;
            btnMusicDJ.Text = "🔥 DJ";
            // 
            // FormFocusMode
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1200, 800);
            Controls.Add(pnlSpotify);
            Controls.Add(pnlSounds);
            Controls.Add(pnlScenes);
            Controls.Add(pnlBottomDock);
            Controls.Add(lblTimer);
            Controls.Add(lblMin);
            Controls.Add(wmpBackground);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormFocusMode";
            Text = "FormFocusMode";
            WindowState = FormWindowState.Maximized;
            Load += FormFocusMode_Load;
            pnlBottomDock.ResumeLayout(false);
            pnlScenes.ResumeLayout(false);
            pnlTabs.ResumeLayout(false);
            pnlSounds.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)wmpBackground).EndInit();
            pnlSpotify.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webViewSpotify).EndInit();
            flpMusicMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}