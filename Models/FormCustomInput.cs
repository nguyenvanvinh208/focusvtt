using System;
using System.Drawing;
using System.Windows.Forms;
namespace Do_an.Forms
{
    public class FormCustomInput : Form
    {
        public string ResultText { get; private set; }
        private TextBox txtInput; private Button btnOk, btnCancel;
        public FormCustomInput(string title, string ph)
        {
            this.FormBorderStyle = FormBorderStyle.None; this.StartPosition = FormStartPosition.CenterParent; this.Size = new Size(400, 200); this.BackColor = Color.White; this.Paint += (s, e) => ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.LightGray, ButtonBorderStyle.Solid);
            Label lbl = new Label { Text = title, Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true }; this.Controls.Add(lbl);
            Panel p = new Panel { Size = new Size(360, 45), Location = new Point(20, 70), BackColor = Color.FromArgb(240, 242, 245) }; this.Controls.Add(p);
            txtInput = new TextBox { BorderStyle = BorderStyle.None, BackColor = Color.FromArgb(240, 242, 245), Font = new Font("Segoe UI", 12), Location = new Point(10, 10), Width = 340, Text = ph }; p.Controls.Add(txtInput);
            txtInput.Enter += (s, e) => { if (txtInput.Text == ph) txtInput.Text = ""; };
            btnOk = new Button { Text = "XÁC NHẬN", Size = new Size(100, 40), Location = new Point(280, 140), FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(0, 132, 255), ForeColor = Color.White };
            btnOk.Click += (s, e) => { ResultText = txtInput.Text; DialogResult = DialogResult.OK; Close(); }; this.Controls.Add(btnOk);
            btnCancel = new Button { Text = "Hủy", Size = new Size(80, 40), Location = new Point(190, 140), FlatStyle = FlatStyle.Flat, BackColor = Color.Transparent, ForeColor = Color.Gray };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); }; this.Controls.Add(btnCancel);
        }
    }
}