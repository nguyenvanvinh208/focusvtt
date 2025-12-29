using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Do_an.Models;

namespace Do_an.Forms
{
    public partial class ThemeCard : UserControl
    {
        public ThemeInfo ThemeData { get; private set; }
        public event Action<ThemeInfo> OnSelectTheme;

        private Image _image;
        private string _title;
        private bool _isHovered = false;

        public ThemeCard()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        public ThemeCard(ThemeInfo theme) : this()
        {
            ThemeData = theme;

            // [ĐÃ SỬA LỖI TẠI ĐÂY]
            // Dùng ThumbnailImg thay vì Thumbnail
            _image = theme.ThumbnailImg;

            _title = theme.Title;

            this.Size = new Size(200, 120);
            this.BackColor = Color.Transparent;
            this.Cursor = Cursors.Hand;

            this.Click += (s, e) => OnSelectTheme?.Invoke(ThemeData);
            this.MouseEnter += (s, e) => { _isHovered = true; Invalidate(); };
            this.MouseLeave += (s, e) => { _isHovered = false; Invalidate(); };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle r = this.ClientRectangle;
            r.Width--; r.Height--;

            using (GraphicsPath path = GetRoundedPath(r, 15))
            {
                g.SetClip(path);
                if (_image != null)
                    g.DrawImage(_image, r);
                else
                    using (SolidBrush b = new SolidBrush(Color.Gray)) g.FillRectangle(b, r);

                Rectangle bottomRect = new Rectangle(0, Height - 40, Width, 40);
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
                {
                    g.FillRectangle(brush, bottomRect);
                }
                g.ResetClip();

                Color borderColor = _isHovered ? Color.Orange : Color.FromArgb(100, 255, 255, 255);
                using (Pen p = new Pen(borderColor, _isHovered ? 3 : 1))
                {
                    g.DrawPath(p, path);
                }
            }

            using (Font f = new Font("Segoe UI", 10, FontStyle.Bold))
            {
                TextRenderer.DrawText(g, _title, f, new Point(10, Height - 30), Color.White);
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