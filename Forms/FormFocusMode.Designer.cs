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
            this.lblMin = new System.Windows.Forms.Label();
            this.lblTimer = new System.Windows.Forms.Label();
            this.pnlBottomDock = new System.Windows.Forms.Panel();
            this.btnScenes = new System.Windows.Forms.Button();
            this.btnMusic = new System.Windows.Forms.Button();
            this.btnSounds = new System.Windows.Forms.Button();
            this.btnToggleTimer = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.pnlScenes = new System.Windows.Forms.Panel();
            this.lblTitleScenes = new System.Windows.Forms.Label();
            this.pnlTabs = new System.Windows.Forms.Panel();
            this.btnTabStatic = new System.Windows.Forms.Button();
            this.btnTabLive = new System.Windows.Forms.Button();
            this.pnlSounds = new System.Windows.Forms.Panel();
            this.lblTitleSounds = new System.Windows.Forms.Label();
            this.flpMixer = new System.Windows.Forms.FlowLayoutPanel();
            this.wmpBackground = new AxWMPLib.AxWindowsMediaPlayer();
            this.pnlSpotify = new System.Windows.Forms.Panel();
            this.webViewSpotify = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.flpMusicMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.btnMusicLofi = new System.Windows.Forms.Button();
            this.btnMusicCafe = new System.Windows.Forms.Button();
            this.btnMusicMTP = new System.Windows.Forms.Button();
            this.btnMusicDJ = new System.Windows.Forms.Button();

            this.pnlBottomDock.SuspendLayout();
            this.pnlScenes.SuspendLayout();
            this.pnlTabs.SuspendLayout();
            this.pnlSounds.SuspendLayout();
            this.pnlSpotify.SuspendLayout();
            this.flpMusicMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wmpBackground)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.webViewSpotify)).BeginInit();
            this.SuspendLayout();

            // 
            // lblMin
            // 
            this.lblMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMin.AutoSize = true;
            this.lblMin.BackColor = System.Drawing.Color.Transparent;
            this.lblMin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMin.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblMin.ForeColor = System.Drawing.Color.White;
            // [ĐÃ SỬA] Đặt sát góc phải (1165) và sát mép trên (0)
            this.lblMin.Location = new System.Drawing.Point(1165, 0);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(35, 46);
            this.lblMin.TabIndex = 10;
            this.lblMin.Text = "_";
            this.lblMin.Click += new System.EventHandler(this.LblMin_Click);

            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.BackColor = System.Drawing.Color.Transparent;
            this.lblTimer.Font = new System.Drawing.Font("Segoe UI", 72F, System.Drawing.FontStyle.Bold);
            this.lblTimer.ForeColor = System.Drawing.Color.White;
            this.lblTimer.Location = new System.Drawing.Point(300, 200);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(554, 159);
            this.lblTimer.TabIndex = 1;
            this.lblTimer.Text = "00:00:00";
            this.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // pnlBottomDock
            // 
            this.pnlBottomDock.BackColor = System.Drawing.Color.Transparent;
            this.pnlBottomDock.Controls.Add(this.btnScenes);
            this.pnlBottomDock.Controls.Add(this.btnMusic);
            this.pnlBottomDock.Controls.Add(this.btnSounds);
            this.pnlBottomDock.Controls.Add(this.btnToggleTimer);
            this.pnlBottomDock.Controls.Add(this.btnFinish);
            this.pnlBottomDock.Location = new System.Drawing.Point(250, 600);
            this.pnlBottomDock.Name = "pnlBottomDock";
            this.pnlBottomDock.Size = new System.Drawing.Size(700, 80);
            this.pnlBottomDock.TabIndex = 5;
            this.pnlBottomDock.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlCommon_Paint);

            // Buttons
            this.btnScenes.Name = "btnScenes"; this.btnScenes.Size = new System.Drawing.Size(100, 60);
            this.btnMusic.Name = "btnMusic"; this.btnMusic.Size = new System.Drawing.Size(100, 60);
            this.btnSounds.Name = "btnSounds"; this.btnSounds.Size = new System.Drawing.Size(100, 60);
            this.btnToggleTimer.Name = "btnToggleTimer"; this.btnToggleTimer.Size = new System.Drawing.Size(100, 60);
            this.btnFinish.Name = "btnFinish"; this.btnFinish.Size = new System.Drawing.Size(100, 60);

            // 
            // pnlScenes
            // 
            this.pnlScenes.BackColor = System.Drawing.Color.Transparent;
            this.pnlScenes.Controls.Add(this.pnlTabs);
            this.pnlScenes.Controls.Add(this.lblTitleScenes);
            this.pnlScenes.Location = new System.Drawing.Point(100, 100);
            this.pnlScenes.Name = "pnlScenes";
            this.pnlScenes.Size = new System.Drawing.Size(800, 500);
            this.pnlScenes.TabIndex = 6;
            this.pnlScenes.Visible = false;
            this.pnlScenes.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlCommon_Paint);

            // lblTitleScenes
            this.lblTitleScenes.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleScenes.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitleScenes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitleScenes.Location = new System.Drawing.Point(0, 0);
            this.lblTitleScenes.Name = "lblTitleScenes";
            this.lblTitleScenes.Size = new System.Drawing.Size(800, 50);
            this.lblTitleScenes.TabIndex = 0;
            this.lblTitleScenes.Text = "Themes Collection";
            this.lblTitleScenes.Padding = new System.Windows.Forms.Padding(20, 10, 0, 0);

            // pnlTabs
            this.pnlTabs.BackColor = System.Drawing.Color.Transparent;
            this.pnlTabs.Controls.Add(this.btnTabStatic);
            this.pnlTabs.Controls.Add(this.btnTabLive);
            this.pnlTabs.Location = new System.Drawing.Point(300, 15);
            this.pnlTabs.Name = "pnlTabs";
            this.pnlTabs.Size = new System.Drawing.Size(300, 40);
            this.pnlTabs.TabIndex = 1;

            // Tab Buttons
            this.btnTabStatic.Dock = System.Windows.Forms.DockStyle.Left; this.btnTabStatic.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnTabStatic.FlatAppearance.BorderSize = 0; this.btnTabStatic.Text = "🖼️ Static Themes"; this.btnTabStatic.Size = new System.Drawing.Size(140, 40);
            this.btnTabLive.Dock = System.Windows.Forms.DockStyle.Right; this.btnTabLive.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnTabLive.FlatAppearance.BorderSize = 0; this.btnTabLive.Text = "▶️ Live Themes"; this.btnTabLive.Size = new System.Drawing.Size(140, 40);

            // 
            // pnlSounds
            // 
            this.pnlSounds.BackColor = System.Drawing.Color.Transparent;
            this.pnlSounds.Controls.Add(this.flpMixer);
            this.pnlSounds.Controls.Add(this.lblTitleSounds);
            this.pnlSounds.Location = new System.Drawing.Point(400, 100);
            this.pnlSounds.Name = "pnlSounds";
            this.pnlSounds.Size = new System.Drawing.Size(350, 350);
            this.pnlSounds.TabIndex = 7;
            this.pnlSounds.Visible = false;
            this.pnlSounds.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlCommon_Paint);

            // lblTitleSounds
            this.lblTitleSounds.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleSounds.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitleSounds.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitleSounds.Location = new System.Drawing.Point(0, 0);
            this.lblTitleSounds.Name = "lblTitleSounds";
            this.lblTitleSounds.Size = new System.Drawing.Size(350, 40);
            this.lblTitleSounds.TabIndex = 0;
            this.lblTitleSounds.Text = "Nature Mixer";
            this.lblTitleSounds.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // flpMixer
            this.flpMixer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMixer.BackColor = System.Drawing.Color.Transparent;
            this.flpMixer.AutoScroll = true;
            this.flpMixer.Location = new System.Drawing.Point(0, 40);
            this.flpMixer.Name = "flpMixer";
            this.flpMixer.Size = new System.Drawing.Size(350, 310);
            this.flpMixer.Padding = new System.Windows.Forms.Padding(20, 10, 0, 0);

            // 
            // pnlSpotify
            // 
            this.pnlSpotify.BackColor = System.Drawing.Color.Transparent;
            this.pnlSpotify.Controls.Add(this.webViewSpotify);
            this.pnlSpotify.Controls.Add(this.flpMusicMenu);
            this.pnlSpotify.Location = new System.Drawing.Point(600, 100);
            this.pnlSpotify.Name = "pnlSpotify";
            this.pnlSpotify.Size = new System.Drawing.Size(580, 400);
            this.pnlSpotify.TabIndex = 8;
            this.pnlSpotify.Visible = false;
            this.pnlSpotify.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlSpotify_Paint);

            // flpMusicMenu
            this.flpMusicMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpMusicMenu.BackColor = System.Drawing.Color.Transparent;
            this.flpMusicMenu.Controls.Add(this.btnMusicLofi);
            this.flpMusicMenu.Controls.Add(this.btnMusicCafe);
            this.flpMusicMenu.Controls.Add(this.btnMusicMTP);
            this.flpMusicMenu.Controls.Add(this.btnMusicDJ);
            this.flpMusicMenu.Location = new System.Drawing.Point(0, 0);
            this.flpMusicMenu.Name = "flpMusicMenu";
            this.flpMusicMenu.Size = new System.Drawing.Size(580, 60);
            this.flpMusicMenu.Padding = new System.Windows.Forms.Padding(30, 12, 0, 0);

            // Music Buttons
            this.btnMusicLofi.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnMusicLofi.FlatAppearance.BorderSize = 0; this.btnMusicLofi.Size = new System.Drawing.Size(100, 35); this.btnMusicLofi.Text = "🎧 Lofi"; this.btnMusicLofi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMusicCafe.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnMusicCafe.FlatAppearance.BorderSize = 0; this.btnMusicCafe.Size = new System.Drawing.Size(100, 35); this.btnMusicCafe.Text = "☕ Cafe"; this.btnMusicCafe.Cursor = System.Windows.Forms.Cursors.Hand; this.btnMusicCafe.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btnMusicMTP.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnMusicMTP.FlatAppearance.BorderSize = 0; this.btnMusicMTP.Size = new System.Drawing.Size(110, 35); this.btnMusicMTP.Text = "🎤 Sơn Tùng"; this.btnMusicMTP.Cursor = System.Windows.Forms.Cursors.Hand; this.btnMusicMTP.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btnMusicDJ.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnMusicDJ.FlatAppearance.BorderSize = 0; this.btnMusicDJ.Size = new System.Drawing.Size(100, 35); this.btnMusicDJ.Text = "🔥 DJ"; this.btnMusicDJ.Cursor = System.Windows.Forms.Cursors.Hand; this.btnMusicDJ.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);

            // webViewSpotify
            this.webViewSpotify.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webViewSpotify.Location = new System.Drawing.Point(0, 60);
            this.webViewSpotify.Name = "webViewSpotify";
            this.webViewSpotify.Size = new System.Drawing.Size(580, 340);
            this.webViewSpotify.TabIndex = 1;
            this.webViewSpotify.ZoomFactor = 0.9D;
            this.webViewSpotify.DefaultBackgroundColor = System.Drawing.Color.FromArgb(30, 30, 30);

            // wmpBackground
            this.wmpBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wmpBackground.Enabled = true;
            this.wmpBackground.Location = new System.Drawing.Point(0, 0);
            this.wmpBackground.Name = "wmpBackground";
            this.wmpBackground.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmpBackground.OcxState")));
            this.wmpBackground.Size = new System.Drawing.Size(1200, 800);
            this.wmpBackground.TabIndex = 0;

            // FormFocusMode
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1200, 800);

            this.Controls.Add(this.pnlSpotify);
            this.Controls.Add(this.pnlSounds);
            this.Controls.Add(this.pnlScenes);
            this.Controls.Add(this.pnlBottomDock);
            this.Controls.Add(this.lblTimer);
            this.Controls.Add(this.lblMin);
            this.Controls.Add(this.wmpBackground);

            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormFocusMode";
            this.Text = "FormFocusMode";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormFocusMode_Load);

            this.pnlBottomDock.ResumeLayout(false);
            this.pnlScenes.ResumeLayout(false);
            this.pnlTabs.ResumeLayout(false);
            this.pnlSounds.ResumeLayout(false);
            this.pnlSpotify.ResumeLayout(false);
            this.flpMusicMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wmpBackground)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.webViewSpotify)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}