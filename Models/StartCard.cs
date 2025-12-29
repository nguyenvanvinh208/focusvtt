using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Do_an.Forms
{
    public class StatCard : Control
    {
        public string Title { get; set; } = "Title";
        public string Value { get; set; } = "0";
        public string Unit { get; set; } = "";

        public Color ColorStart { get; set; } = Color.Orange;
        public Color ColorEnd { get; set; } = Color.Red;

        public StatCard()
        {
            this.DoubleBuffered = true;
            this.Size = new Size(180, 100);
            this.ForeColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 1. Vẽ nền Gradient bo tròn
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            using (GraphicsPath path = GetRoundedPath(rect, 20))
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, ColorStart, ColorEnd, 45f))
            {
                g.FillPath(brush, path);
            }

            // 2. Vẽ Title (Tiêu đề nhỏ ở trên)
            using (Font fTitle = new Font("Segoe UI", 9, FontStyle.Bold))
            {
                g.DrawString(Title, fTitle, Brushes.WhiteSmoke, new Point(15, 15));
            }

            // 3. Vẽ Value (Số liệu lớn ở giữa)
            using (Font fValue = new Font("Segoe UI", 22, FontStyle.Bold))
            {
                g.DrawString(Value, fValue, Brushes.White, new Point(10, 35));
            }

            // 4. Vẽ Unit (Đơn vị + Icon ở dưới) - [SỬA LỖI Ô VUÔNG TẠI ĐÂY]
            if (!string.IsNullOrEmpty(Unit))
            {
                // Sử dụng Font "Segoe UI Emoji" để hỗ trợ hiển thị icon màu
                // Sử dụng TextRenderer.DrawText thay vì g.DrawString để render icon tốt hơn trên WinForms
                using (Font fUnit = new Font("Segoe UI Emoji", 10, FontStyle.Regular))
                {
                    TextRenderer.DrawText(g, Unit, fUnit, new Point(15, 75), Color.WhiteSmoke, TextFormatFlags.NoPadding);
                }
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float d = radius * 2F;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}