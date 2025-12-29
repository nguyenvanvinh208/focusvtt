namespace Do_an.Forms
{
    partial class MainDashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.btnCloseMenu = new System.Windows.Forms.Label();
            this.btnChat = new System.Windows.Forms.Button();
            this.btnRanking = new System.Windows.Forms.Button();
            this.btnSchedule = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.pnlUserInfo = new System.Windows.Forms.Panel();
            this.lblUsername = new System.Windows.Forms.Label();
            this.picAvatar = new System.Windows.Forms.PictureBox();
            this.pnlContent = new System.Windows.Forms.Panel();

            this.pnlSidebar.SuspendLayout();
            this.pnlUserInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            this.SuspendLayout();

            this.pnlContent.BackColor = System.Drawing.Color.Transparent;
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1200, 750);
            this.pnlContent.TabIndex = 0;

            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(101, 67, 33);

            this.pnlSidebar.Controls.Add(this.btnCloseMenu);
            this.pnlSidebar.Controls.Add(this.btnChat);
            this.pnlSidebar.Controls.Add(this.btnRanking);
            this.pnlSidebar.Controls.Add(this.btnSchedule);
            this.pnlSidebar.Controls.Add(this.btnHome);
            this.pnlSidebar.Controls.Add(this.pnlUserInfo);

            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(300, 750);
            this.pnlSidebar.TabIndex = 1;
            this.pnlSidebar.Visible = false;
            this.pnlSidebar.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                (((System.Windows.Forms.AnchorStyles.Top |
                   System.Windows.Forms.AnchorStyles.Bottom)
                   | System.Windows.Forms.AnchorStyles.Left)));


            this.btnCloseMenu.AutoSize = true;
            this.btnCloseMenu.BackColor = System.Drawing.Color.Transparent;
            this.btnCloseMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCloseMenu.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.btnCloseMenu.ForeColor = System.Drawing.Color.NavajoWhite;
            this.btnCloseMenu.Location = new System.Drawing.Point(260, 10);
            this.btnCloseMenu.Name = "btnCloseMenu";
            this.btnCloseMenu.Size = new System.Drawing.Size(27, 30);
            this.btnCloseMenu.Text = "X";
            this.btnCloseMenu.Click += new System.EventHandler(this.BtnCloseMenu_Click);

            // ===== BUTTONS =====
            SetupSidebarButton(this.btnHome, "   Trang Chủ", 220, this.btnHome_Click);
            SetupSidebarButton(this.btnSchedule, "   Lịch / Phòng", 280, this.btnSchedule_Click);
            SetupSidebarButton(this.btnRanking, "   Xếp Hạng", 340, this.btnRanking_Click);
            SetupSidebarButton(this.btnChat, "   Nhắn Tin", 400, this.btnChat_Click);


            this.pnlUserInfo.BackColor = System.Drawing.Color.Transparent;
            this.pnlUserInfo.Controls.Add(this.lblUsername);
            this.pnlUserInfo.Controls.Add(this.picAvatar);
            this.pnlUserInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlUserInfo.Size = new System.Drawing.Size(300, 200);


            this.picAvatar.Location = new System.Drawing.Point(100, 40);
            this.picAvatar.Size = new System.Drawing.Size(100, 100);
            this.picAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAvatar.Click += new System.EventHandler(this.PicAvatar_Click);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, 100, 100);
            this.picAvatar.Region = new System.Drawing.Region(gp);

            this.lblUsername.ForeColor = System.Drawing.Color.NavajoWhite;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUsername.Location = new System.Drawing.Point(0, 150);
            this.lblUsername.Size = new System.Drawing.Size(300, 30);
            this.lblUsername.Text = "Welcome";


            this.BackColor = System.Drawing.Color.FromArgb(253, 248, 235);
            this.ClientSize = new System.Drawing.Size(1200, 750);
            this.Controls.Add(this.pnlSidebar);
            this.Controls.Add(this.pnlContent);

            this.Name = "MainDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FOCUS VTT - Dashboard";

            this.pnlSidebar.ResumeLayout(false);
            this.pnlSidebar.PerformLayout();
            this.pnlUserInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            this.ResumeLayout(false);
        }

        private void SetupSidebarButton(
            System.Windows.Forms.Button btn,
            string text, int y,
            System.EventHandler onClick)
        {
            btn.Text = text;
            btn.Location = new System.Drawing.Point(0, y);
            btn.Size = new System.Drawing.Size(300, 50);
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = System.Drawing.Color.Transparent;

            btn.ForeColor = System.Drawing.Color.NavajoWhite;
            btn.Font = new System.Drawing.Font("Segoe UI", 11F);
            btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
            btn.Click += onClick;

            this.pnlSidebar.Controls.Add(btn);
        }

        #endregion

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlUserInfo;
        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnSchedule;
        private System.Windows.Forms.Button btnRanking;
        private System.Windows.Forms.Button btnChat;
        private System.Windows.Forms.Label btnCloseMenu;
    }
}