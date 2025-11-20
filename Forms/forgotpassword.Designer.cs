namespace Do_an.Forms
{
    partial class forgotpassword
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
            pnlContainer = new Panel();
            llbBackToLogin = new LinkLabel();
            btnSend = new Button();
            panelLine1 = new Panel();
            txtEmail = new TextBox();
            picEmail = new PictureBox();
            lblTitle = new Label();
            pictureBox1 = new PictureBox();
            lblClose = new Label();
            pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picEmail).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pnlContainer
            // 
            pnlContainer.BackColor = Color.White;
            pnlContainer.Controls.Add(llbBackToLogin);
            pnlContainer.Controls.Add(btnSend);
            pnlContainer.Controls.Add(panelLine1);
            pnlContainer.Controls.Add(txtEmail);
            pnlContainer.Controls.Add(picEmail);
            pnlContainer.Controls.Add(lblTitle);
            pnlContainer.Location = new Point(32, 87);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new Size(343, 353);
            pnlContainer.TabIndex = 0;
            // 
            // llbBackToLogin
            // 
            llbBackToLogin.AutoSize = true;
            llbBackToLogin.Font = new Font("Segoe UI", 9F);
            llbBackToLogin.LinkColor = Color.RoyalBlue;
            llbBackToLogin.Location = new Point(124, 284);
            llbBackToLogin.Name = "llbBackToLogin";
            llbBackToLogin.Size = new Size(138, 20);
            llbBackToLogin.TabIndex = 3;
            llbBackToLogin.TabStop = true;
            llbBackToLogin.Text = "Quay lại đăng nhập";
            llbBackToLogin.LinkClicked += llbBackToLogin_LinkClicked;
            // 
            // btnSend
            // 
            btnSend.BackColor = Color.RoyalBlue;
            btnSend.Cursor = Cursors.Hand;
            btnSend.FlatAppearance.BorderSize = 0;
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSend.ForeColor = Color.White;
            btnSend.Location = new Point(46, 218);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(300, 39);
            btnSend.TabIndex = 2;
            btnSend.Text = "GỬI MÃ";
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += btnSend_Click;
            // 
            // panelLine1
            // 
            panelLine1.BackColor = Color.DarkGray;
            panelLine1.Location = new Point(34, 158);
            panelLine1.Name = "panelLine1";
            panelLine1.Size = new Size(338, 1);
            panelLine1.TabIndex = 4;
            // 
            // txtEmail
            // 
            txtEmail.BorderStyle = BorderStyle.None;
            txtEmail.Font = new Font("Segoe UI", 11F);
            txtEmail.ForeColor = Color.FromArgb(64, 64, 64);
            txtEmail.Location = new Point(73, 128);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(292, 25);
            txtEmail.TabIndex = 1;
            txtEmail.Text = "Nhập email đăng ký...";
            // 
            // picEmail
            // 
            picEmail.BackColor = Color.Transparent;
            picEmail.Image = Properties.Resources.email;
            picEmail.Location = new Point(34, 126);
            picEmail.Name = "picEmail";
            picEmail.Size = new Size(28, 26);
            picEmail.SizeMode = PictureBoxSizeMode.StretchImage;
            picEmail.TabIndex = 5;
            picEmail.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.RoyalBlue;
            lblTitle.Location = new Point(68, 42);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(237, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Quên Mật Khẩu";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.snapedit_1763483114039;
            pictureBox1.Location = new Point(-10, -66);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1407, 708);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // lblClose
            // 
            lblClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblClose.AutoSize = true;
            lblClose.BackColor = Color.RoyalBlue;
            lblClose.Cursor = Cursors.Hand;
            lblClose.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            lblClose.ForeColor = Color.White;
            lblClose.Location = new Point(1087, 9);
            lblClose.Name = "lblClose";
            lblClose.Size = new Size(31, 29);
            lblClose.TabIndex = 5;
            lblClose.Text = "X";
            lblClose.Click += lblClose_Click;
            // 
            // forgotpassword
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1135, 579);
            Controls.Add(lblClose);
            Controls.Add(pnlContainer);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "forgotpassword";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "forgotpassword";
            pnlContainer.ResumeLayout(false);
            pnlContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picEmail).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Panel panelLine1;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.LinkLabel llbBackToLogin;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblClose;
    }
}