namespace Do_an.Forms
{
    partial class FormScoreHistory
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblClose = new System.Windows.Forms.Label();
            this.picAvatar = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblTitleHeader = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblXP = new System.Windows.Forms.Label();
            this.line = new System.Windows.Forms.Panel();
            this.pnlStats = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            this.SuspendLayout();
            // lblClose
            this.lblClose.AutoSize = true;
            this.lblClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblClose.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblClose.ForeColor = System.Drawing.Color.White;
            this.lblClose.Location = new System.Drawing.Point(860, 10);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(24, 25);
            this.lblClose.TabIndex = 0;
            this.lblClose.Text = "X";
            this.lblClose.Click += new System.EventHandler(this.LblClose_Click);
            // picAvatar
            this.picAvatar.Location = new System.Drawing.Point(50, 50);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Size = new System.Drawing.Size(120, 120);
            this.picAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAvatar.TabIndex = 1;
            this.picAvatar.TabStop = false;
            this.picAvatar.Paint += new System.Windows.Forms.PaintEventHandler(this.PicAvatar_Paint);
            // lblName
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(190, 50);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(100, 32);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "NAME";
            // lblTitleHeader
            this.lblTitleHeader.AutoSize = true;
            this.lblTitleHeader.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitleHeader.ForeColor = System.Drawing.Color.Gold;
            this.lblTitleHeader.Location = new System.Drawing.Point(190, 90);
            this.lblTitleHeader.Name = "lblTitleHeader";
            this.lblTitleHeader.Size = new System.Drawing.Size(239, 20);
            this.lblTitleHeader.TabIndex = 3;
            this.lblTitleHeader.Text = "THỐNG KÊ XẾP HẠNG && ĐIỂM SỐ";
            // lblLevel
            this.lblLevel.AutoSize = true;
            this.lblLevel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblLevel.ForeColor = System.Drawing.Color.Cyan;
            this.lblLevel.Location = new System.Drawing.Point(190, 125);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(46, 21);
            this.lblLevel.TabIndex = 4;
            this.lblLevel.Text = "Lv. 1";
            // lblXP
            this.lblXP.AutoSize = true;
            this.lblXP.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblXP.ForeColor = System.Drawing.Color.Gray;
            this.lblXP.Location = new System.Drawing.Point(250, 127);
            this.lblXP.Name = "lblXP";
            this.lblXP.Size = new System.Drawing.Size(56, 15);
            this.lblXP.TabIndex = 5;
            this.lblXP.Text = "0/100 XP";
            // line
            this.line.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.line.Location = new System.Drawing.Point(50, 190);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(800, 1);
            this.line.TabIndex = 6;
            // pnlStats
            this.pnlStats.Location = new System.Drawing.Point(50, 220);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Size = new System.Drawing.Size(800, 250);
            this.pnlStats.TabIndex = 7;
            // FormScoreHistory
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.pnlStats);
            this.Controls.Add(this.line);
            this.Controls.Add(this.lblXP);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.lblTitleHeader);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.picAvatar);
            this.Controls.Add(this.lblClose);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormScoreHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormScoreHistory";
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblTitleHeader;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblXP;
        private System.Windows.Forms.Panel line;
        private System.Windows.Forms.Panel pnlStats;
    }
}