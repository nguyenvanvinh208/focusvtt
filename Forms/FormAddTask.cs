using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Do_an.Models;

namespace Do_an.Forms
{
    public partial class FormAddTask : Form
    {
        public string TaskNameResult { get; private set; }
        public DateTime StartTimeResult { get; private set; }
        public DateTime EndTimeResult { get; private set; }

        private DateTime _fixedDate;
        private DateTime _tempStartTime;
        private DateTime _tempEndTime;
        private List<TaskInfo> _existingTasks;

        // Control nhập liệu (Chỉ còn Giờ và Phút)
        private TextBox txtStartHour, txtStartMin;
        private TextBox txtEndHour, txtEndMin;

        // Màu sắc
        private readonly Color clrCyan = Color.FromArgb(0, 229, 255);
        private readonly Color clrPurple = Color.FromArgb(124, 77, 255);
        private readonly Color clrPink = Color.FromArgb(233, 30, 99);
        private readonly Color clrBgPanel = Color.FromArgb(25, 20, 40);
        private readonly Color clrBorderStart = Color.FromArgb(0, 255, 255);
        private readonly Color clrBorderEnd = Color.FromArgb(255, 0, 255);

        public FormAddTask(DateTime targetDate, List<TaskInfo> existingTasks = null)
        {
            InitializeComponent();
            this.KeyPreview = true;

            _fixedDate = targetDate.Date;
            _existingTasks = existingTasks ?? new List<TaskInfo>();

            DateTime now = DateTime.Now;
            if (_fixedDate.Date == now.Date)
                _tempStartTime = _fixedDate.AddHours(now.Hour).AddMinutes(now.Minute);
            else
                _tempStartTime = _fixedDate.AddHours(8).AddMinutes(0);

            _tempEndTime = _tempStartTime.AddHours(1);

            SetupDirectInput(pnlStart, true);
            SetupDirectInput(pnlEnd, false);

            DisplayTimeOnInputs();
        }

        // --- HÀM TẠO GIAO DIỆN 24H (CĂN GIỮA) ---
        private void SetupDirectInput(Panel parent, bool isStart)
        {
            Font fontNum = new Font("Segoe UI", 18F, FontStyle.Bold);

            // 1. TextBox Giờ (0-23)
            // [FIX]: X=40 (Căn lề trái để vào giữa khung)
            TextBox txtHour = CreateTimeTextBox(40, 28, fontNum);
            txtHour.TextChanged += (s, e) => ValidateNumberInput(txtHour, 0, 23);
            txtHour.KeyDown += (s, e) => HandleTextBoxKeyDown(txtHour, e, 0, 23);
            txtHour.Leave += (s, e) => ValidateAndFormat(txtHour, 0, 23);
            parent.Controls.Add(txtHour);

            // 2. Dấu hai chấm (:)
            // [FIX]: X=105
            Label lblSep = new Label();
            lblSep.Text = ":";
            lblSep.ForeColor = Color.White;
            lblSep.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblSep.AutoSize = true;
            lblSep.Location = new System.Drawing.Point(105, 26);
            parent.Controls.Add(lblSep);

            // 3. TextBox Phút (0-59)
            // [FIX]: X=120
            TextBox txtMin = CreateTimeTextBox(120, 28, fontNum);
            txtMin.TextChanged += (s, e) => ValidateNumberInput(txtMin, 0, 59);
            txtMin.KeyDown += (s, e) => HandleTextBoxKeyDown(txtMin, e, 0, 59);
            txtMin.Leave += (s, e) => ValidateAndFormat(txtMin, 0, 59);
            parent.Controls.Add(txtMin);

            // Không còn Label AM/PM nữa
            if (isStart) { txtStartHour = txtHour; txtStartMin = txtMin; }
            else { txtEndHour = txtHour; txtEndMin = txtMin; }
        }

        private TextBox CreateTimeTextBox(int x, int y, Font f)
        {
            TextBox txt = new TextBox();
            txt.BorderStyle = BorderStyle.None;
            txt.BackColor = clrBgPanel;
            txt.ForeColor = Color.White;
            txt.Font = f;
            txt.Width = 60;
            txt.Location = new System.Drawing.Point(x, y);
            txt.TextAlign = HorizontalAlignment.Center;
            txt.MaxLength = 2;

            txt.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
            };

            txt.Click += (s, e) => txt.SelectAll();

            return txt;
        }

        // Xử lý phím Lên/Xuống
        private void HandleTextBoxKeyDown(TextBox txt, KeyEventArgs e, int min, int max)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                int current = int.Parse(string.IsNullOrEmpty(txt.Text) ? "0" : txt.Text);

                if (e.KeyCode == Keys.Up) current++;
                else current--;

                if (current > max) current = min;
                if (current < min) current = max;

                txt.Text = current.ToString("00");
                txt.SelectAll();

                SyncTimeFromUI();
            }
        }

        private void ValidateNumberInput(TextBox txt, int min, int max)
        {
            if (int.TryParse(txt.Text, out int val))
            {
                // Nếu nhập > max (ví dụ 25h), tự sửa về max
                if (val > max) txt.Text = max.ToString("00");
            }
        }

        private void ValidateAndFormat(TextBox txt, int min, int max)
        {
            if (int.TryParse(txt.Text, out int val))
            {
                if (val > max) val = max;
                if (val < min) val = min;
                txt.Text = val.ToString("00");
            }
            else
            {
                txt.Text = min.ToString("00");
            }
            SyncTimeFromUI();
        }

        private void DisplayTimeOnInputs()
        {
            // Hiển thị dạng 24h (HH)
            txtStartHour.Text = _tempStartTime.ToString("HH");
            txtStartMin.Text = _tempStartTime.ToString("mm");

            txtEndHour.Text = _tempEndTime.ToString("HH");
            txtEndMin.Text = _tempEndTime.ToString("mm");
        }

        private void SyncTimeFromUI()
        {
            try
            {
                _tempStartTime = GetDateTimeFromUI(txtStartHour, txtStartMin);
                _tempEndTime = GetDateTimeFromUI(txtEndHour, txtEndMin);
            }
            catch { }
        }

        private DateTime GetDateTimeFromUI(TextBox txtH, TextBox txtM)
        {
            int h = int.Parse(string.IsNullOrEmpty(txtH.Text) ? "0" : txtH.Text);
            int m = int.Parse(string.IsNullOrEmpty(txtM.Text) ? "0" : txtM.Text);

            // Giờ 24h nên không cần xử lý AM/PM, lấy trực tiếp
            return new DateTime(_fixedDate.Year, _fixedDate.Month, _fixedDate.Day, h, m, 0);
        }

        // --- NÚT LƯU ---
        private void btnSave_Click(object sender, EventArgs e)
        {
            SyncTimeFromUI();

            if (string.IsNullOrWhiteSpace(txtTaskName.Text) || txtTaskName.Text == "Tên công việc")
            {
                MessageBox.Show("Vui lòng nhập tên công việc!", "Thông báo"); return;
            }
            if (_tempEndTime <= _tempStartTime)
            {
                MessageBox.Show("Giờ kết thúc phải sau giờ bắt đầu!", "Lỗi thời gian"); return;
            }
            if (_fixedDate.Date == DateTime.Now.Date && _tempStartTime < DateTime.Now.AddMinutes(-1))
            {
                MessageBox.Show("Không thể đặt giờ quá khứ!", "Lỗi thời gian"); return;
            }
            foreach (var task in _existingTasks)
            {
                if (_tempStartTime < task.End && _tempEndTime > task.Start)
                {
                    MessageBox.Show($"Trùng lịch với: {task.Name}\n({task.Start:HH:mm} - {task.End:HH:mm})", "Lỗi trùng lịch"); return;
                }
            }

            TaskNameResult = txtTaskName.Text;
            StartTimeResult = _tempStartTime;
            EndTimeResult = _tempEndTime;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // --- UI HELPERS ---
        private void lblClose_Click(object sender, EventArgs e) => this.Close();
        private void btnCancel_Click(object sender, EventArgs e) => this.Close();
        private void txtTaskName_Enter(object sender, EventArgs e) { if (txtTaskName.Text == "Tên công việc") { txtTaskName.Text = ""; txtTaskName.ForeColor = Color.White; } }
        private void txtTaskName_Leave(object sender, EventArgs e) { if (string.IsNullOrWhiteSpace(txtTaskName.Text)) { txtTaskName.Text = "Tên công việc"; txtTaskName.ForeColor = Color.Gray; } }

        // --- VẼ TRANG TRÍ (KHÔNG CÒN MŨI TÊN, KHÔNG CÒN ĐƯỜNG KẺ TRẮNG) ---
        private void pnlTime_Paint(object sender, PaintEventArgs e)
        {
            Panel pnl = sender as Panel;
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Vẽ viền Tím
            DrawRoundedBorder(g, pnl.ClientRectangle, clrPurple, 15);

            // Vẽ Icon Đồng hồ
            using (Pen p = new Pen(clrCyan, 2))
            {
                g.DrawEllipse(p, 8, 42, 16, 16);
                g.DrawLine(p, 16, 50, 16, 46); g.DrawLine(p, 16, 50, 20, 52);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e); Graphics g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath p = GetRoundedPath(ClientRectangle, 30)) Region = new Region(p);
            Rectangle rb = new Rectangle(1, 1, Width - 2, Height - 2);
            using (GraphicsPath p = GetRoundedPath(rb, 30)) using (LinearGradientBrush b = new LinearGradientBrush(rb, clrBorderStart, clrBorderEnd, 45f)) using (Pen pen = new Pen(b, 3)) g.DrawPath(pen, p);
            using (LinearGradientBrush b = new LinearGradientBrush(new Point(0, 55), new Point(Width, 55), clrBorderStart, clrBorderEnd)) g.FillRectangle(b, 0, 55, Width, 2);
        }
        private void btnSave_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias; Rectangle r = btnSave.ClientRectangle; r.Inflate(-1, -1);
            using (GraphicsPath p = GetRoundedPath(r, 30)) using (SolidBrush b = new SolidBrush(clrPink)) g.FillPath(b, p);
            TextRenderer.DrawText(g, btnSave.Text, btnSave.Font, r, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
        private void btnCancel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias; Rectangle r = btnCancel.ClientRectangle; r.Inflate(-1, -1);
            using (GraphicsPath p = GetRoundedPath(r, 30)) using (Pen pen = new Pen(Color.White, 2)) g.DrawPath(pen, p);
            TextRenderer.DrawText(g, btnCancel.Text, btnCancel.Font, r, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
        private void pnlName_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias; DrawRoundedBorder(g, pnlName.ClientRectangle, clrCyan, 15);
            using (Pen p = new Pen(Color.Gray, 2)) { g.DrawEllipse(p, 15, 18, 14, 14); g.DrawLine(p, 27, 30, 34, 37); }
        }
        private void DrawRoundedBorder(Graphics g, Rectangle r, Color c, int rad)
        {
            r.Width -= 1; r.Height -= 1; using (GraphicsPath p = GetRoundedPath(r, rad)) using (Pen pen = new Pen(c, 1.5f)) g.DrawPath(pen, p);
        }
        private GraphicsPath GetRoundedPath(Rectangle r, int d)
        {
            GraphicsPath p = new GraphicsPath(); d *= 2;
            p.AddArc(r.X, r.Y, d, d, 180, 90); p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90); p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure(); return p;
        }
    }
}