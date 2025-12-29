namespace Do_an.Forms
{
    partial class FormProfile
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            picAvatar = new PictureBox();
            lblName = new Label();
            lblLevel = new Label();
            lblXP = new Label();
            btnAction = new Button();
            btnCancel = new Button();
            pnlStats = new Panel();
            pnlEdit = new Panel();
            lblClose = new Label();
            line = new Panel();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            SuspendLayout();
            // 
            // picAvatar
            // 
            picAvatar.Location = new Point(67, 77);
            picAvatar.Margin = new Padding(4, 5, 4, 5);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(160, 185);
            picAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
            picAvatar.TabIndex = 0;
            picAvatar.TabStop = false;
            picAvatar.Paint += PicAvatar_Paint;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblName.ForeColor = Color.White;
            lblName.Location = new Point(253, 77);
            lblName.Margin = new Padding(4, 0, 4, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(108, 41);
            lblName.TabIndex = 1;
            lblName.Text = "NAME";
            // 
            // lblLevel
            // 
            lblLevel.AutoSize = true;
            lblLevel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblLevel.ForeColor = Color.Cyan;
            lblLevel.Location = new Point(253, 146);
            lblLevel.Margin = new Padding(4, 0, 4, 0);
            lblLevel.Name = "lblLevel";
            lblLevel.Size = new Size(54, 28);
            lblLevel.TabIndex = 2;
            lblLevel.Text = "Lv. 1";
            // 
            // lblXP
            // 
            lblXP.AutoSize = true;
            lblXP.Font = new Font("Segoe UI", 9F);
            lblXP.ForeColor = Color.Gray;
            lblXP.Location = new Point(865, 154);
            lblXP.Margin = new Padding(4, 0, 4, 0);
            lblXP.Name = "lblXP";
            lblXP.Size = new Size(68, 20);
            lblXP.TabIndex = 3;
            lblXP.Text = "0/100 XP";
            // 
            // btnAction
            // 
            btnAction.BackColor = Color.FromArgb(40, 40, 50);
            btnAction.Cursor = Cursors.Hand;
            btnAction.FlatAppearance.BorderSize = 0;
            btnAction.FlatStyle = FlatStyle.Flat;
            btnAction.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAction.ForeColor = Color.White;
            btnAction.Location = new Point(800, 77);
            btnAction.Margin = new Padding(4, 5, 4, 5);
            btnAction.Name = "btnAction";
            btnAction.Size = new Size(133, 54);
            btnAction.TabIndex = 4;
            btnAction.Text = "Chỉnh sửa";
            btnAction.UseVisualStyleBackColor = false;
            btnAction.Click += BtnAction_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(40, 40, 50);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(640, 77);
            btnCancel.Margin = new Padding(4, 5, 4, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(133, 54);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Hủy bỏ";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Visible = false;
            btnCancel.Click += BtnCancel_Click;
            // 
            // pnlStats
            // 
            pnlStats.Location = new Point(67, 323);
            pnlStats.Margin = new Padding(4, 5, 4, 5);
            pnlStats.Name = "pnlStats";
            pnlStats.Size = new Size(933, 385);
            pnlStats.TabIndex = 6;
            // 
            // pnlEdit
            // 
            pnlEdit.Location = new Point(67, 323);
            pnlEdit.Margin = new Padding(4, 5, 4, 5);
            pnlEdit.Name = "pnlEdit";
            pnlEdit.Size = new Size(933, 385);
            pnlEdit.TabIndex = 7;
            pnlEdit.Visible = false;
            // 
            // lblClose
            // 
            lblClose.AutoSize = true;
            lblClose.Cursor = Cursors.Hand;
            lblClose.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblClose.ForeColor = Color.White;
            lblClose.Location = new Point(1013, 15);
            lblClose.Margin = new Padding(4, 0, 4, 0);
            lblClose.Name = "lblClose";
            lblClose.Size = new Size(30, 32);
            lblClose.TabIndex = 8;
            lblClose.Text = "X";
            lblClose.Click += LblClose_Click;
            // 
            // line
            // 
            line.BackColor = Color.FromArgb(50, 255, 255, 255);
            line.Location = new Point(67, 292);
            line.Margin = new Padding(4, 5, 4, 5);
            line.Name = "line";
            line.Size = new Size(933, 2);
            line.TabIndex = 9;
            // 
            // FormProfile
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(10, 10, 20);
            ClientSize = new Size(1200, 923);
            Controls.Add(line);
            Controls.Add(lblClose);
            Controls.Add(pnlEdit);
            Controls.Add(pnlStats);
            Controls.Add(btnCancel);
            Controls.Add(btnAction);
            Controls.Add(lblXP);
            Controls.Add(lblLevel);
            Controls.Add(lblName);
            Controls.Add(picAvatar);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "FormProfile";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormProfile";
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblXP;
        private System.Windows.Forms.Button btnAction;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.Panel pnlEdit;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.Panel line;
    }
}