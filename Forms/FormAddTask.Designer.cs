namespace Do_an.Forms
{
    partial class FormAddTask
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblClose = new System.Windows.Forms.Label();
            this.pnlName = new System.Windows.Forms.Panel();
            this.txtTaskName = new System.Windows.Forms.TextBox();
            this.pnlStart = new System.Windows.Forms.Panel();
            this.lblHeaderStart = new System.Windows.Forms.Label();
            this.pnlEnd = new System.Windows.Forms.Panel();
            this.lblHeaderEnd = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlName.SuspendLayout();
            this.pnlStart.SuspendLayout();
            this.pnlEnd.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(550, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Thêm Công Việc";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblClose
            // 
            this.lblClose.AutoSize = true;
            this.lblClose.BackColor = System.Drawing.Color.Transparent;
            this.lblClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblClose.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblClose.ForeColor = System.Drawing.Color.White;
            this.lblClose.Location = new System.Drawing.Point(500, 10);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(33, 37);
            this.lblClose.TabIndex = 1;
            this.lblClose.Text = "✕";
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            // 
            // pnlName
            // 
            this.pnlName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(20)))), ((int)(((byte)(40)))));
            this.pnlName.Controls.Add(this.txtTaskName);
            this.pnlName.Location = new System.Drawing.Point(40, 80);
            this.pnlName.Name = "pnlName";
            this.pnlName.Size = new System.Drawing.Size(470, 55);
            this.pnlName.TabIndex = 2;
            this.pnlName.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlName_Paint);
            // 
            // txtTaskName
            // 
            this.txtTaskName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(20)))), ((int)(((byte)(40)))));
            this.txtTaskName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTaskName.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtTaskName.ForeColor = System.Drawing.Color.Gray;
            this.txtTaskName.Location = new System.Drawing.Point(55, 14);
            this.txtTaskName.Name = "txtTaskName";
            this.txtTaskName.Size = new System.Drawing.Size(390, 30);
            this.txtTaskName.TabIndex = 0;
            this.txtTaskName.Text = "Tên công việc";
            this.txtTaskName.Enter += new System.EventHandler(this.txtTaskName_Enter);
            this.txtTaskName.Leave += new System.EventHandler(this.txtTaskName_Leave);
            // 
            // pnlStart
            // 
            this.pnlStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(20)))), ((int)(((byte)(40)))));
            this.pnlStart.Controls.Add(this.lblHeaderStart);
            this.pnlStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlStart.Location = new System.Drawing.Point(40, 160);
            this.pnlStart.Name = "pnlStart";
            this.pnlStart.Size = new System.Drawing.Size(220, 90);
            this.pnlStart.TabIndex = 3;
            this.pnlStart.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTime_Paint);
            // 
            // lblHeaderStart
            // 
            this.lblHeaderStart.AutoSize = true;
            this.lblHeaderStart.BackColor = System.Drawing.Color.Transparent;
            this.lblHeaderStart.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHeaderStart.ForeColor = System.Drawing.Color.LightGray;
            this.lblHeaderStart.Location = new System.Drawing.Point(70, 8);
            this.lblHeaderStart.Name = "lblHeaderStart";
            this.lblHeaderStart.Size = new System.Drawing.Size(101, 23);
            this.lblHeaderStart.TabIndex = 0;
            this.lblHeaderStart.Text = "Giờ bắt đầu";
            // 
            // pnlEnd
            // 
            this.pnlEnd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(20)))), ((int)(((byte)(40)))));
            this.pnlEnd.Controls.Add(this.lblHeaderEnd);
            this.pnlEnd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlEnd.Location = new System.Drawing.Point(290, 160);
            this.pnlEnd.Name = "pnlEnd";
            this.pnlEnd.Size = new System.Drawing.Size(220, 90);
            this.pnlEnd.TabIndex = 4;
            this.pnlEnd.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTime_Paint);
            // 
            // lblHeaderEnd
            // 
            this.lblHeaderEnd.AutoSize = true;
            this.lblHeaderEnd.BackColor = System.Drawing.Color.Transparent;
            this.lblHeaderEnd.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHeaderEnd.ForeColor = System.Drawing.Color.LightGray;
            this.lblHeaderEnd.Location = new System.Drawing.Point(70, 8);
            this.lblHeaderEnd.Name = "lblHeaderEnd";
            this.lblHeaderEnd.Size = new System.Drawing.Size(105, 23);
            this.lblHeaderEnd.TabIndex = 0;
            this.lblHeaderEnd.Text = "Giờ kết thúc";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(100, 290);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 55);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "HỦY";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.Paint += new System.Windows.Forms.PaintEventHandler(this.btnCancel_Paint);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(300, 290);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 55);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "LƯU";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.Paint += new System.Windows.Forms.PaintEventHandler(this.btnSave_Paint);

            // 
            // FormAddTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(20)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(550, 380);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pnlEnd);
            this.Controls.Add(this.pnlStart);
            this.Controls.Add(this.pnlName);
            this.Controls.Add(this.lblClose);
            this.Controls.Add(this.lblTitle);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormAddTask";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormAddTask";
            this.pnlName.ResumeLayout(false);
            this.pnlName.PerformLayout();
            this.pnlStart.ResumeLayout(false);
            this.pnlStart.PerformLayout();
            this.pnlEnd.ResumeLayout(false);
            this.pnlEnd.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.Panel pnlName;
        private System.Windows.Forms.TextBox txtTaskName;
        private System.Windows.Forms.Panel pnlStart;
        private System.Windows.Forms.Label lblHeaderStart;
        private System.Windows.Forms.Panel pnlEnd;
        private System.Windows.Forms.Label lblHeaderEnd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}