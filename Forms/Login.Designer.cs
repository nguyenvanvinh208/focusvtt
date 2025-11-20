namespace Do_an.Forms
{
    partial class Login
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            label1 = new Label();
            pnlContainer = new Panel();
            chkShowPassword = new CheckBox();
            llbRegister = new LinkLabel();
            btnLogin = new Button();
            llbQuenMatKhau = new LinkLabel();
            panelLine2 = new Panel();
            txtPassword = new TextBox();
            picLock = new PictureBox();
            panelLine1 = new Panel();
            txtUsername = new TextBox();
            picUser = new PictureBox();
            lblTitle = new Label();
            pictureBox1 = new PictureBox();
            lblClose = new Label();
            pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picUser).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.BackColor = Color.IndianRed;
            label1.Cursor = Cursors.Hand;
            label1.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(457, 133);
            label1.Name = "label1";
            label1.Size = new Size(31, 29);
            label1.TabIndex = 1;
            label1.Text = "X";
            // 
            // pnlContainer
            // 
            pnlContainer.BackColor = Color.White;
            pnlContainer.Controls.Add(chkShowPassword);
            pnlContainer.Controls.Add(llbRegister);
            pnlContainer.Controls.Add(label1);
            pnlContainer.Controls.Add(btnLogin);
            pnlContainer.Controls.Add(llbQuenMatKhau);
            pnlContainer.Controls.Add(panelLine2);
            pnlContainer.Controls.Add(txtPassword);
            pnlContainer.Controls.Add(picLock);
            pnlContainer.Controls.Add(panelLine1);
            pnlContainer.Controls.Add(txtUsername);
            pnlContainer.Controls.Add(picUser);
            pnlContainer.Controls.Add(lblTitle);
            pnlContainer.Location = new Point(28, 71);
            pnlContainer.Margin = new Padding(3, 4, 3, 4);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new Size(357, 504);
            pnlContainer.TabIndex = 4;
            // 
            // chkShowPassword
            // 
            chkShowPassword.AutoSize = true;
            chkShowPassword.Cursor = Cursors.Hand;
            chkShowPassword.FlatStyle = FlatStyle.Flat;
            chkShowPassword.ForeColor = Color.Gray;
            chkShowPassword.Location = new Point(300, 294);
            chkShowPassword.Margin = new Padding(3, 4, 3, 4);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(14, 13);
            chkShowPassword.TabIndex = 10;
            chkShowPassword.UseVisualStyleBackColor = true;
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged_1;
            // 
            // llbRegister
            // 
            llbRegister.AutoSize = true;
            llbRegister.Location = new Point(119, 481);
            llbRegister.Name = "llbRegister";
            llbRegister.Size = new Size(112, 20);
            llbRegister.TabIndex = 4;
            llbRegister.TabStop = true;
            llbRegister.Text = "Đăng ký tại đây";
            llbRegister.LinkClicked += llbRegister_LinkClicked_1;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.RoyalBlue;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(40, 400);
            btnLogin.Margin = new Padding(3, 4, 3, 4);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(270, 56);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click_1;
            // 
            // llbQuenMatKhau
            // 
            llbQuenMatKhau.AutoSize = true;
            llbQuenMatKhau.Font = new Font("Segoe UI", 9F);
            llbQuenMatKhau.LinkColor = Color.RoyalBlue;
            llbQuenMatKhau.Location = new Point(190, 350);
            llbQuenMatKhau.Name = "llbQuenMatKhau";
            llbQuenMatKhau.Size = new Size(116, 20);
            llbQuenMatKhau.TabIndex = 2;
            llbQuenMatKhau.TabStop = true;
            llbQuenMatKhau.Text = "Quên mật khẩu?";
            llbQuenMatKhau.LinkClicked += llbQuenMatKhau_LinkClicked_1;
            // 
            // panelLine2
            // 
            panelLine2.BackColor = Color.DarkGray;
            panelLine2.Location = new Point(40, 325);
            panelLine2.Margin = new Padding(3, 4, 3, 4);
            panelLine2.Name = "panelLine2";
            panelLine2.Size = new Size(270, 1);
            panelLine2.TabIndex = 6;
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Segoe UI", 11F);
            txtPassword.ForeColor = Color.FromArgb(64, 64, 64);
            txtPassword.Location = new Point(80, 290);
            txtPassword.Margin = new Padding(3, 4, 3, 4);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(210, 25);
            txtPassword.TabIndex = 1;
            txtPassword.Text = "password";
            txtPassword.UseSystemPasswordChar = true;
            // 
            // picLock
            // 
            picLock.BackColor = Color.Transparent;
            picLock.Image = Properties.Resources._lock;
            picLock.Location = new Point(40, 281);
            picLock.Margin = new Padding(3, 4, 3, 4);
            picLock.Name = "picLock";
            picLock.Size = new Size(30, 38);
            picLock.SizeMode = PictureBoxSizeMode.StretchImage;
            picLock.TabIndex = 4;
            picLock.TabStop = false;
            // 
            // panelLine1
            // 
            panelLine1.BackColor = Color.DarkGray;
            panelLine1.Location = new Point(40, 238);
            panelLine1.Margin = new Padding(3, 4, 3, 4);
            panelLine1.Name = "panelLine1";
            panelLine1.Size = new Size(270, 1);
            panelLine1.TabIndex = 3;
            // 
            // txtUsername
            // 
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Font = new Font("Segoe UI", 11F);
            txtUsername.ForeColor = Color.FromArgb(64, 64, 64);
            txtUsername.Location = new Point(80, 202);
            txtUsername.Margin = new Padding(3, 4, 3, 4);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(230, 25);
            txtUsername.TabIndex = 0;
            txtUsername.Text = "nvquang";
            // 
            // picUser
            // 
            picUser.BackColor = Color.Transparent;
            picUser.Image = Properties.Resources.user;
            picUser.Location = new Point(40, 194);
            picUser.Margin = new Padding(3, 4, 3, 4);
            picUser.Name = "picUser";
            picUser.Size = new Size(30, 38);
            picUser.SizeMode = PictureBoxSizeMode.StretchImage;
            picUser.TabIndex = 1;
            picUser.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.RoyalBlue;
            lblTitle.Location = new Point(75, 62);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(200, 46);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Đăng Nhập";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.snapedit_1763483114039;
            pictureBox1.Location = new Point(-95, -21);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1457, 713);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // lblClose
            // 
            lblClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblClose.AutoSize = true;
            lblClose.BackColor = Color.RoyalBlue;
            lblClose.Cursor = Cursors.Hand;
            lblClose.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            lblClose.ForeColor = Color.White;
            lblClose.Location = new Point(997, 0);
            lblClose.Name = "lblClose";
            lblClose.Size = new Size(31, 29);
            lblClose.TabIndex = 5;
            lblClose.Text = "X";
            lblClose.Click += lblClose_Click_1;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1040, 635);
            Controls.Add(lblClose);
            Controls.Add(pnlContainer);
            Controls.Add(pictureBox1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            pnlContainer.ResumeLayout(false);
            pnlContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLock).EndInit();
            ((System.ComponentModel.ISupportInitialize)picUser).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private Label label1;
        private Panel pnlContainer;
        private CheckBox chkShowPassword;
        private LinkLabel llbRegister;
        private Button btnLogin;
        private LinkLabel llbQuenMatKhau;
        private Panel panelLine2;
        private TextBox txtPassword;
        private PictureBox picLock;
        private Panel panelLine1;
        private TextBox txtUsername;
        private PictureBox picUser;
        private Label lblTitle;
        private PictureBox pictureBox1;
        private Label lblClose;
    }
}
