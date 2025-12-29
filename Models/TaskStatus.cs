using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Do_an.Models
{
    public enum TaskStatus { Future, Running, Missed, Done }
    public class NeonTaskCard : System.Windows.Forms.Control
    {
        public string TaskName; public DateTime StartTime; public DateTime EndTime; public bool IsDone;
        public TaskStatus CurrentStatus { get; private set; }

        public NeonTaskCard()
        {
            DoubleBuffered = true;
            SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);
            BackColor = System.Drawing.Color.Transparent;
            Cursor = System.Windows.Forms.Cursors.Hand;
            this.Height = 35;
        }

        public void SetupData(string name, DateTime start, DateTime end, bool done)
        {
            TaskName = name; StartTime = start; EndTime = end; IsDone = done;
            UpdateStatus(); Invalidate();
        }

        public void UpdateStatus()
        {
            DateTime now = DateTime.Now;
            if (IsDone) CurrentStatus = TaskStatus.Done;
            else if (now >= StartTime && now <= EndTime) CurrentStatus = TaskStatus.Running;
            else if (now > EndTime) CurrentStatus = TaskStatus.Missed;
            else CurrentStatus = TaskStatus.Future;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            UpdateStatus();
            System.Drawing.Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            System.Drawing.Color border = System.Drawing.Color.Gray, fill = System.Drawing.Color.FromArgb(200, 20, 20, 30), text = System.Drawing.Color.White;
            switch (CurrentStatus)
            {
                case TaskStatus.Done: border = System.Drawing.Color.SpringGreen; fill = System.Drawing.Color.FromArgb(100, 0, 50, 0); break;
                case TaskStatus.Running: border = System.Drawing.Color.Gold; fill = System.Drawing.Color.FromArgb(200, 100, 80, 0); text = System.Drawing.Color.Yellow; break;
                case TaskStatus.Missed: border = System.Drawing.Color.Red; text = System.Drawing.Color.Silver; break;
                case TaskStatus.Future: border = System.Drawing.Color.Cyan; break;
            }

            using (System.Drawing.Drawing2D.GraphicsPath p = CreateRoundedPath(new System.Drawing.Rectangle(0, 0, Width - 1, Height - 1), 8))
            {
                using (System.Drawing.SolidBrush b = new System.Drawing.SolidBrush(fill)) g.FillPath(b, p);
                using (System.Drawing.Pen pen = new System.Drawing.Pen(border, 1.5f)) g.DrawPath(pen, p);
            }

            using (System.Drawing.SolidBrush b = new System.Drawing.SolidBrush(border)) g.FillEllipse(b, 10, 12, 8, 8);
            string s = $"{TaskName.ToUpper()} ({StartTime:HH:mm}-{EndTime:HH:mm})";
            using (System.Drawing.Font f = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold))
                System.Windows.Forms.TextRenderer.DrawText(g, s, f, new System.Drawing.Point(25, 9), text);
        }

        private System.Drawing.Drawing2D.GraphicsPath CreateRoundedPath(System.Drawing.Rectangle r, int d)
        {
            System.Drawing.Drawing2D.GraphicsPath p = new System.Drawing.Drawing2D.GraphicsPath(); d *= 2;
            p.AddArc(r.X, r.Y, d, d, 180, 90); p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90); p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure(); return p;
        }
    }
}
