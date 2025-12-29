namespace Do_an.Forms
{
    partial class UC_Home
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMenu = new System.Windows.Forms.Label();
            this.picSlider = new System.Windows.Forms.PictureBox();
            this.lblFeatureDesc = new System.Windows.Forms.Label();
            this.lblFeatureTitle = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSlider)).BeginInit();
            this.picSlider.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            // Đặt màu nền trong suốt để nhìn thấy ảnh bên dưới
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblMenu);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1000, 62);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Black; // Đổi thành màu đen
            this.lblTitle.Location = new System.Drawing.Point(60, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(130, 32);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Trang Chủ";
            // 
            // lblMenu
            // 
            this.lblMenu.AutoSize = true;
            this.lblMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenu.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblMenu.ForeColor = System.Drawing.Color.Black; // Đổi thành màu đen
            this.lblMenu.Location = new System.Drawing.Point(15, 12);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(41, 37);
            this.lblMenu.TabIndex = 0;
            this.lblMenu.Text = "☰";
            // 
            // picSlider
            // 
            this.picSlider.BackColor = System.Drawing.Color.White;
            // Quan trọng: Đưa pnlHeader vào trong picSlider
            this.picSlider.Controls.Add(this.pnlHeader);
            this.picSlider.Controls.Add(this.lblFeatureDesc);
            this.picSlider.Controls.Add(this.lblFeatureTitle);
            this.picSlider.Controls.Add(this.btnNext);
            this.picSlider.Controls.Add(this.btnPrev);
            this.picSlider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picSlider.Location = new System.Drawing.Point(0, 0);
            this.picSlider.Name = "picSlider";
            this.picSlider.Size = new System.Drawing.Size(1000, 812);
            this.picSlider.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage; // Giữ nguyên Stretch để full ảnh
            this.picSlider.TabIndex = 1;
            this.picSlider.TabStop = false;
            // 
            // lblFeatureDesc
            // 
            this.lblFeatureDesc.AutoSize = true;
            this.lblFeatureDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblFeatureDesc.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblFeatureDesc.ForeColor = System.Drawing.Color.Black; // Đổi thành màu đen
            this.lblFeatureDesc.Location = new System.Drawing.Point(105, 350);
            this.lblFeatureDesc.Name = "lblFeatureDesc";
            this.lblFeatureDesc.Size = new System.Drawing.Size(215, 32);
            this.lblFeatureDesc.TabIndex = 3;
            this.lblFeatureDesc.Text = "Mô tả chức năng....";
            // 
            // lblFeatureTitle
            // 
            this.lblFeatureTitle.AutoSize = true;
            this.lblFeatureTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblFeatureTitle.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold);
            this.lblFeatureTitle.ForeColor = System.Drawing.Color.Black; // Đổi thành màu đen
            this.lblFeatureTitle.Location = new System.Drawing.Point(100, 250);
            this.lblFeatureTitle.Name = "lblFeatureTitle";
            this.lblFeatureTitle.Size = new System.Drawing.Size(388, 81);
            this.lblFeatureTitle.TabIndex = 2;
            this.lblFeatureTitle.Text = "TÊN TIÊU ĐỀ";
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.BackColor = System.Drawing.Color.Transparent;
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.FlatAppearance.BorderColor = System.Drawing.Color.Black; // Viền đen
            this.btnNext.FlatAppearance.BorderSize = 2;
            this.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))); // Hiệu ứng nhấn màu tối
            this.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Consolas", 20F, System.Drawing.FontStyle.Bold);
            this.btnNext.ForeColor = System.Drawing.Color.Black; // Chữ đen
            this.btnNext.Location = new System.Drawing.Point(920, 338);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(60, 75);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.BackColor = System.Drawing.Color.Transparent;
            this.btnPrev.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrev.FlatAppearance.BorderColor = System.Drawing.Color.Black; // Viền đen
            this.btnPrev.FlatAppearance.BorderSize = 2;
            this.btnPrev.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnPrev.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Consolas", 20F, System.Drawing.FontStyle.Bold);
            this.btnPrev.ForeColor = System.Drawing.Color.Black; // Chữ đen
            this.btnPrev.Location = new System.Drawing.Point(20, 338);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(60, 75);
            this.btnPrev.TabIndex = 0;
            this.btnPrev.Text = "<";
            this.btnPrev.UseVisualStyleBackColor = false;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // UC_Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White; // Nền UC màu trắng
            // Chỉ add picSlider vào Controls của UC, pnlHeader đã nằm trong picSlider
            this.Controls.Add(this.picSlider);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UC_Home";
            this.Size = new System.Drawing.Size(1000, 812);
            this.Load += new System.EventHandler(this.UC_Home_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSlider)).EndInit();
            this.picSlider.ResumeLayout(false);
            this.picSlider.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picSlider;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblFeatureTitle;
        private System.Windows.Forms.Label lblFeatureDesc;
    }
}