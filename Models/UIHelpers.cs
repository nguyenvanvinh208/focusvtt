using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Do_an.Forms
{
    public class CircularPictureBox : PictureBox
    {
        protected override void OnPaint(PaintEventArgs pe)
        {
            GraphicsPath gp = new GraphicsPath(); gp.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height); this.Region = new Region(gp); base.OnPaint(pe);
            using (Pen p = new Pen(Color.LightGray, 1)) { pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias; pe.Graphics.DrawEllipse(p, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1); }
        }
    }
    public class RoundedButton : Button
    {
        public int BorderRadius { get; set; } = 20; public Color BackgroundColor { get; set; } = Color.FromArgb(240, 242, 245); public Color TextColor { get; set; } = Color.Black;
        public RoundedButton() { this.FlatStyle = FlatStyle.Flat; this.FlatAppearance.BorderSize = 0; this.BackColor = Color.Transparent; this.Cursor = Cursors.Hand; }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias; Rectangle rect = ClientRectangle; rect.Width--; rect.Height--;
            using (GraphicsPath path = GetRoundedPath(rect, BorderRadius)) using (SolidBrush brush = new SolidBrush(BackgroundColor)) { this.Region = new Region(path); g.FillPath(brush, path); }
            TextRenderer.DrawText(g, this.Text, this.Font, rect, TextColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
        private GraphicsPath GetRoundedPath(Rectangle r, int radius) { GraphicsPath p = new GraphicsPath(); p.AddArc(r.X, r.Y, radius, radius, 180, 90); p.AddArc(r.Right - radius, r.Y, radius, radius, 270, 90); p.AddArc(r.Right - radius, r.Bottom - radius, radius, radius, 0, 90); p.AddArc(r.X, r.Bottom - radius, radius, radius, 90, 90); p.CloseFigure(); return p; }
    }
}