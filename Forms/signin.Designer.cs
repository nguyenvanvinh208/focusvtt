namespace Do_an.Forms
{
    partial class signin // <-- Đã đổi tên thành signin
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
            pnlContainer = new Panel();
            chkShowPassword = new CheckBox();
            llbLogin = new LinkLabel();
            btnSignup = new Button();
            panelLine4 = new Panel();
            txtConfirmPassword = new TextBox();
            picConfirm = new PictureBox();
            panelLine3 = new Panel();
            txtPassword = new TextBox();
            picLock = new PictureBox();
            panelLine2 = new Panel();
            txtEmail = new TextBox();
            picEmail = new PictureBox();
            panelLine1 = new Panel();
            txtUsername = new TextBox();
            picUser = new PictureBox();
            lblTitle = new Label();
            pictureBox1 = new PictureBox();
            lblClose = new Label();
            pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picConfirm).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picEmail).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picUser).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pnlContainer
            // 
            pnlContainer.BackColor = Color.White;
            pnlContainer.Controls.Add(chkShowPassword);
            pnlContainer.Controls.Add(llbLogin);
            pnlContainer.Controls.Add(btnSignup);
            pnlContainer.Controls.Add(panelLine4);
            pnlContainer.Controls.Add(txtConfirmPassword);
            pnlContainer.Controls.Add(picConfirm);
            pnlContainer.Controls.Add(panelLine3);
            pnlContainer.Controls.Add(txtPassword);
            pnlContainer.Controls.Add(picLock);
            pnlContainer.Controls.Add(panelLine2);
            pnlContainer.Controls.Add(txtEmail);
            pnlContainer.Controls.Add(picEmail);
            pnlContainer.Controls.Add(panelLine1);
            pnlContainer.Controls.Add(txtUsername);
            pnlContainer.Controls.Add(picUser);
            pnlContainer.Controls.Add(lblTitle);
            pnlContainer.Location = new Point(31, 56);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.Size = new Size(360, 475);
            pnlContainer.TabIndex = 0;
            // 
            // chkShowPassword
            // 
            chkShowPassword.AutoSize = true;
            chkShowPassword.Cursor = Cursors.Hand;
            chkShowPassword.FlatStyle = FlatStyle.Flat;
            chkShowPassword.ForeColor = Color.Gray;
            chkShowPassword.Location = new Point(315, 225);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(14, 13);
            chkShowPassword.TabIndex = 10;
            chkShowPassword.UseVisualStyleBackColor = true;
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
            // 
            // llbLogin
            // 
            llbLogin.AutoSize = true;
            llbLogin.Font = new Font("Segoe UI", 9F);
            llbLogin.LinkColor = Color.RoyalBlue;
            llbLogin.Location = new Point(85, 450);
            llbLogin.Name = "llbLogin";
            llbLogin.Size = new Size(197, 20);
            llbLogin.TabIndex = 6;
            llbLogin.TabStop = true;
            llbLogin.Text = "Đã có tài khoản? Đăng nhập";
            llbLogin.LinkClicked += llbLogin_LinkClicked;
            // 
            // btnSignup
            // 
            btnSignup.BackColor = Color.RoyalBlue;
            btnSignup.Cursor = Cursors.Hand;
            btnSignup.FlatAppearance.BorderSize = 0;
            btnSignup.FlatStyle = FlatStyle.Flat;
            btnSignup.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSignup.ForeColor = Color.White;
            btnSignup.Location = new Point(30, 380);
            btnSignup.Name = "btnSignup";
            btnSignup.Size = new Size(300, 45);
            btnSignup.TabIndex = 5;
            btnSignup.Text = "Đăng Ký";
            btnSignup.UseVisualStyleBackColor = false;
            btnSignup.Click += btnSignup_Click;
            // 
            // panelLine4
            // 
            panelLine4.BackColor = Color.DarkGray;
            panelLine4.Location = new Point(30, 310);
            panelLine4.Name = "panelLine4";
            panelLine4.Size = new Size(300, 1);
            panelLine4.TabIndex = 9;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.BorderStyle = BorderStyle.None;
            txtConfirmPassword.Font = new Font("Segoe UI", 11F);
            txtConfirmPassword.ForeColor = Color.FromArgb(64, 64, 64);
            txtConfirmPassword.Location = new Point(65, 282);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(240, 25);
            txtConfirmPassword.TabIndex = 4;
            txtConfirmPassword.Text = "Confirm Password";
            txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // picConfirm
            // 
            picConfirm.BackColor = Color.Transparent;
            picConfirm.Image = Properties.Resources.authentication;
            picConfirm.Location = new Point(30, 280);
            picConfirm.Name = "picConfirm";
            picConfirm.Size = new Size(25, 25);
            picConfirm.SizeMode = PictureBoxSizeMode.StretchImage;
            picConfirm.TabIndex = 8;
            picConfirm.TabStop = false;
            // 
            // panelLine3
            // 
            panelLine3.BackColor = Color.DarkGray;
            panelLine3.Location = new Point(30, 250);
            panelLine3.Name = "panelLine3";
            panelLine3.Size = new Size(300, 1);
            panelLine3.TabIndex = 7;
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Segoe UI", 11F);
            txtPassword.ForeColor = Color.FromArgb(64, 64, 64);
            txtPassword.Location = new Point(65, 222);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(240, 25);
            txtPassword.TabIndex = 3;
            txtPassword.Text = "Password";
            txtPassword.UseSystemPasswordChar = true;
            // 
            // picLock
            // 
            picLock.BackColor = Color.Transparent;
            picLock.Image = Properties.Resources._lock;
            picLock.Location = new Point(30, 220);
            picLock.Name = "picLock";
            picLock.Size = new Size(25, 25);
            picLock.SizeMode = PictureBoxSizeMode.StretchImage;
            picLock.TabIndex = 6;
            picLock.TabStop = false;
            // 
            // panelLine2
            // 
            panelLine2.BackColor = Color.DarkGray;
            panelLine2.Location = new Point(30, 190);
            panelLine2.Name = "panelLine2";
            panelLine2.Size = new Size(300, 1);
            panelLine2.TabIndex = 5;
            // 
            // txtEmail
            // 
            txtEmail.BorderStyle = BorderStyle.None;
            txtEmail.Font = new Font("Segoe UI", 11F);
            txtEmail.ForeColor = Color.FromArgb(64, 64, 64);
            txtEmail.Location = new Point(65, 162);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(260, 25);
            txtEmail.TabIndex = 2;
            txtEmail.Text = "Email";
            // 
            // picEmail
            // 
            picEmail.BackColor = Color.Transparent;
            picEmail.Image = Properties.Resources.email;
            picEmail.Location = new Point(30, 160);
            picEmail.Name = "picEmail";
            picEmail.Size = new Size(25, 25);
            picEmail.SizeMode = PictureBoxSizeMode.StretchImage;
            picEmail.TabIndex = 4;
            picEmail.TabStop = false;
            // 
            // panelLine1
            // 
            panelLine1.BackColor = Color.DarkGray;
            panelLine1.Location = new Point(30, 130);
            panelLine1.Name = "panelLine1";
            panelLine1.Size = new Size(300, 1);
            panelLine1.TabIndex = 3;
            // 
            // txtUsername
            // 
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Font = new Font("Segoe UI", 11F);
            txtUsername.ForeColor = Color.FromArgb(64, 64, 64);
            txtUsername.Location = new Point(65, 102);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(260, 25);
            txtUsername.TabIndex = 1;
            txtUsername.Text = "Username";
            // 
            // picUser
            // 
            picUser.BackColor = Color.Transparent;
            picUser.Image = Properties.Resources.user;
            picUser.Location = new Point(30, 100);
            picUser.Name = "picUser";
            picUser.Size = new Size(25, 25);
            picUser.SizeMode = PictureBoxSizeMode.StretchImage;
            picUser.TabIndex = 2;
            picUser.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.RoyalBlue;
            lblTitle.Location = new Point(110, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(153, 46);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Đăng Ký";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.snapedit_1763483114039;
            pictureBox1.Location = new Point(-290, -35);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1828, 704);
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
            lblClose.Location = new Point(1104, 9);
            lblClose.Name = "lblClose";
            lblClose.Size = new Size(31, 29);
            lblClose.TabIndex = 5;
            lblClose.Text = "X";
            lblClose.Click += lblClose_Click;
            // 
            // signin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1147, 616);
            Controls.Add(lblClose);
            Controls.Add(pnlContainer);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "signin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "signin";
            pnlContainer.ResumeLayout(false);
            pnlContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picConfirm).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLock).EndInit();
            ((System.ComponentModel.ISupportInitialize)picEmail).EndInit();
            ((System.ComponentModel.ISupportInitialize)picUser).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picUser;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Panel panelLine1;
        private System.Windows.Forms.PictureBox picEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Panel panelLine2;
        private System.Windows.Forms.PictureBox picLock;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Panel panelLine3;
        private System.Windows.Forms.PictureBox picConfirm;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Panel panelLine4;
        private System.Windows.Forms.Button btnSignup;
        private System.Windows.Forms.LinkLabel llbLogin;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblClose;
    }
}