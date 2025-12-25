using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using Do_an.Models;
namespace Do_an.Forms
{
    public class FormRequestList : Form
    {
        public string SelectedUid { get; private set; }
        public FormRequestList(List<User> reqs)
        {
            this.FormBorderStyle = FormBorderStyle.None; this.StartPosition = FormStartPosition.CenterParent; this.Size = new Size(400, 500); this.BackColor = Color.White; this.Paint += (s, e) => ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.LightGray, ButtonBorderStyle.Solid);
            Label lbl = new Label { Text = "LỜI MỜI KẾT BẠN", Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true }; this.Controls.Add(lbl);
            Button x = new Button { Text = "✕", FlatStyle = FlatStyle.Flat, Location = new Point(350, 10), Size = new Size(40, 40), ForeColor = Color.Gray, FlatAppearance = { BorderSize = 0 } }; x.Click += (s, e) => Close(); this.Controls.Add(x);
            FlowLayoutPanel flp = new FlowLayoutPanel { Location = new Point(10, 70), Size = new Size(380, 420), AutoScroll = true, FlowDirection = FlowDirection.TopDown, WrapContents = false }; this.Controls.Add(flp);
            foreach (var u in reqs) AddItem(u, flp);
        }
        private void AddItem(User u, FlowLayoutPanel flp)
        {
            Panel p = new Panel { Size = new Size(350, 70), BackColor = Color.White };
            PictureBox pic = new PictureBox { Size = new Size(50, 50), Location = new Point(10, 10), SizeMode = PictureBoxSizeMode.StretchImage };
            GraphicsPath gp = new GraphicsPath(); gp.AddEllipse(0, 0, 50, 50); pic.Region = new Region(gp);
            try { string path = Path.Combine(Application.StartupPath, "UserAvatars", u.Uid + ".jpg"); if (File.Exists(path)) pic.Image = Image.FromFile(path); else pic.BackColor = Color.LightGray; } catch { pic.BackColor = Color.LightGray; }
            p.Controls.Add(pic);
            Label n = new Label { Text = u.Username ?? u.Email, Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(70, 15), AutoSize = true }; p.Controls.Add(n);
            Button b = new Button { Text = "Chấp nhận", BackColor = Color.FromArgb(49, 162, 76), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(100, 35), Location = new Point(240, 17) };
            b.Click += (s, e) => { SelectedUid = u.Uid; DialogResult = DialogResult.OK; Close(); }; p.Controls.Add(b);
            flp.Controls.Add(p);
        }
    }
}