namespace Do_an.Forms
{
    partial class UC_Ranking
    {
        private System.ComponentModel.IContainer components = null;

        // Header
        private System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Label lblTitle;

        // Tabs
        private System.Windows.Forms.Panel pnlTabs;
        private System.Windows.Forms.Button btnTabGlobal;
        private System.Windows.Forms.Button btnTabFriends;

        // Podium
        private System.Windows.Forms.Panel pnlPodium;

        // List
        private System.Windows.Forms.Panel pnlHeaderRow;
        private System.Windows.Forms.Label lblH_Rank;
        private System.Windows.Forms.Label lblH_Info;
        private System.Windows.Forms.Label lblH_Lvl;
        private System.Windows.Forms.Label lblH_Time;

        private System.Windows.Forms.FlowLayoutPanel flpLeaderboard;
        private System.Windows.Forms.Panel pnlMyRank;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblMenu = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlTabs = new System.Windows.Forms.Panel();
            this.btnTabGlobal = new System.Windows.Forms.Button();
            this.btnTabFriends = new System.Windows.Forms.Button();
            this.pnlPodium = new System.Windows.Forms.Panel();
            this.pnlHeaderRow = new System.Windows.Forms.Panel();
            this.lblH_Rank = new System.Windows.Forms.Label();
            this.lblH_Info = new System.Windows.Forms.Label();
            this.lblH_Lvl = new System.Windows.Forms.Label();
            this.lblH_Time = new System.Windows.Forms.Label();
            this.flpLeaderboard = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlMyRank = new System.Windows.Forms.Panel();
            this.pnlTabs.SuspendLayout();
            this.pnlHeaderRow.SuspendLayout();
            this.SuspendLayout();

            // 
            // UC_Ranking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            // Màu nền mặc định nếu chưa có ảnh nền (Màu gỗ tối)
            this.BackColor = System.Drawing.Color.FromArgb(40, 30, 20);
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DoubleBuffered = true;
            this.Controls.Add(this.lblMenu);
            this.Controls.Add(this.pnlMyRank);
            this.Controls.Add(this.flpLeaderboard);
            this.Controls.Add(this.pnlHeaderRow);
            this.Controls.Add(this.pnlPodium);
            this.Controls.Add(this.pnlTabs);
            this.Controls.Add(this.lblTitle);
            this.Name = "UC_Ranking";
            this.Size = new System.Drawing.Size(1000, 700);
            this.Load += new System.EventHandler(this.UC_Ranking_Load);

            // 
            // lblMenu
            // 
            this.lblMenu.AutoSize = true;
            this.lblMenu.BackColor = System.Drawing.Color.Transparent;
            this.lblMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenu.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblMenu.ForeColor = System.Drawing.Color.White;
            this.lblMenu.Location = new System.Drawing.Point(15, 15);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(40, 46);
            this.lblMenu.TabIndex = 10;
            this.lblMenu.Text = "☰";
            this.lblMenu.Click += new System.EventHandler(this.LblMenu_Click);

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(70, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(305, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "BẢNG XẾP HẠNG";

            // 
            // pnlTabs
            // 
            this.pnlTabs.BackColor = System.Drawing.Color.Transparent;
            this.pnlTabs.Controls.Add(this.btnTabGlobal);
            this.pnlTabs.Controls.Add(this.btnTabFriends);
            this.pnlTabs.Location = new System.Drawing.Point(650, 20);
            this.pnlTabs.Name = "pnlTabs";
            this.pnlTabs.Size = new System.Drawing.Size(300, 40);
            this.pnlTabs.TabIndex = 1;

            // 
            // btnTabGlobal
            // 
            this.btnTabGlobal.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnTabGlobal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabGlobal.FlatAppearance.BorderSize = 1;
            this.btnTabGlobal.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnTabGlobal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabGlobal.ForeColor = System.Drawing.Color.White;
            this.btnTabGlobal.Location = new System.Drawing.Point(0, 0);
            this.btnTabGlobal.Name = "btnTabGlobal";
            this.btnTabGlobal.Size = new System.Drawing.Size(140, 40);
            this.btnTabGlobal.TabIndex = 0;
            this.btnTabGlobal.Text = "TOÀN CẦU";
            this.btnTabGlobal.UseVisualStyleBackColor = true;

            // 
            // btnTabFriends
            // 
            this.btnTabFriends.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnTabFriends.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTabFriends.FlatAppearance.BorderSize = 1;
            this.btnTabFriends.FlatAppearance.BorderColor = System.Drawing.Color.Goldenrod;
            this.btnTabFriends.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTabFriends.ForeColor = System.Drawing.Color.Silver;
            this.btnTabFriends.Location = new System.Drawing.Point(160, 0);
            this.btnTabFriends.Name = "btnTabFriends";
            this.btnTabFriends.Size = new System.Drawing.Size(140, 40);
            this.btnTabFriends.TabIndex = 1;
            this.btnTabFriends.Text = "BẠN BÈ";
            this.btnTabFriends.UseVisualStyleBackColor = true;

            // 
            // pnlPodium
            // 
            this.pnlPodium.BackColor = System.Drawing.Color.Transparent;
            this.pnlPodium.Location = new System.Drawing.Point(0, 80);
            this.pnlPodium.Name = "pnlPodium";
            this.pnlPodium.Size = new System.Drawing.Size(1000, 350);
            this.pnlPodium.TabIndex = 2;
            this.pnlPodium.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPodium_Paint);

            // 
            // pnlHeaderRow
            // 
            this.pnlHeaderRow.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeaderRow.Controls.Add(this.lblH_Time);
            this.pnlHeaderRow.Controls.Add(this.lblH_Lvl);
            this.pnlHeaderRow.Controls.Add(this.lblH_Info);
            this.pnlHeaderRow.Controls.Add(this.lblH_Rank);
            this.pnlHeaderRow.Location = new System.Drawing.Point(50, 440);
            this.pnlHeaderRow.Name = "pnlHeaderRow";
            this.pnlHeaderRow.Size = new System.Drawing.Size(900, 30);
            this.pnlHeaderRow.TabIndex = 3;
            // 
            // lblH_Rank
            // 
            this.lblH_Rank.Text = "#";
            this.lblH_Rank.ForeColor = System.Drawing.Color.Silver;
            this.lblH_Rank.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
            this.lblH_Rank.AutoSize = true;
            // 
            // lblH_Info
            // 
            this.lblH_Info.Text = "THÀNH VIÊN";
            this.lblH_Info.ForeColor = System.Drawing.Color.Silver;
            this.lblH_Info.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
            this.lblH_Info.AutoSize = true;
            // 
            // lblH_Lvl
            // 
            this.lblH_Lvl.Text = "LEVEL";
            this.lblH_Lvl.ForeColor = System.Drawing.Color.Silver;
            this.lblH_Lvl.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
            this.lblH_Lvl.AutoSize = true;
            // 
            // lblH_Time
            // 
            this.lblH_Time.Text = "THỜI GIAN";
            this.lblH_Time.ForeColor = System.Drawing.Color.Silver;
            this.lblH_Time.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
            this.lblH_Time.AutoSize = true;

            // 
            // flpLeaderboard
            // 
            this.flpLeaderboard.AutoScroll = true;
            this.flpLeaderboard.BackColor = System.Drawing.Color.Transparent;
            this.flpLeaderboard.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpLeaderboard.Location = new System.Drawing.Point(30, 470);
            this.flpLeaderboard.Name = "flpLeaderboard";
            this.flpLeaderboard.Size = new System.Drawing.Size(940, 180);
            this.flpLeaderboard.TabIndex = 4;
            this.flpLeaderboard.WrapContents = false;

            // 
            // pnlMyRank
            // 
            this.pnlMyRank.BackColor = System.Drawing.Color.Transparent;
            this.pnlMyRank.Location = new System.Drawing.Point(30, 660);
            this.pnlMyRank.Name = "pnlMyRank";
            this.pnlMyRank.Size = new System.Drawing.Size(940, 80);
            this.pnlMyRank.TabIndex = 5;
            this.pnlMyRank.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMyRank_Paint);

            this.pnlTabs.ResumeLayout(false);
            this.pnlHeaderRow.ResumeLayout(false);
            this.pnlHeaderRow.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}