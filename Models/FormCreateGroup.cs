using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Do_an.Firebase;
using Do_an.Models;
namespace Do_an.Forms
{
    public class FormCreateGroup : Form
    {
        private TextBox txtName; private CheckedListBox clb; private Button btnOk, btnCan;
        private FirebaseDatabaseService _db = new FirebaseDatabaseService(); private string _uid; private List<User> _friends = new List<User>();
        public FormCreateGroup(string uid) { _uid = uid; Init(); LoadF(); }
        private void Init()
        {
            this.Size = new Size(400, 500); this.StartPosition = FormStartPosition.CenterParent; this.FormBorderStyle = FormBorderStyle.None; this.BackColor = Color.White; this.Paint += (s, e) => ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.LightGray, ButtonBorderStyle.Solid);
            Controls.Add(new Label { Text = "TẠO NHÓM MỚI", Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true });
            Controls.Add(new Label { Text = "Tên nhóm:", Location = new Point(20, 60), AutoSize = true });
            txtName = new TextBox { Location = new Point(20, 85), Width = 360, Font = new Font("Segoe UI", 11), BackColor = Color.FromArgb(240, 242, 245), BorderStyle = BorderStyle.FixedSingle }; Controls.Add(txtName);
            Controls.Add(new Label { Text = "Chọn thành viên:", Location = new Point(20, 120), AutoSize = true });
            clb = new CheckedListBox { Location = new Point(20, 145), Width = 360, Height = 280, BorderStyle = BorderStyle.FixedSingle, CheckOnClick = true }; Controls.Add(clb);
            btnOk = new Button { Text = "TẠO", BackColor = Color.FromArgb(0, 132, 255), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(100, 40), Location = new Point(280, 440) }; btnOk.Click += Ok; Controls.Add(btnOk);
            btnCan = new Button { Text = "Hủy", BackColor = Color.White, ForeColor = Color.Gray, FlatStyle = FlatStyle.Flat, Size = new Size(80, 40), Location = new Point(190, 440) }; btnCan.Click += (s, e) => Close(); Controls.Add(btnCan);
        }
        private async void LoadF() { try { var f = await _db.GetFriendsAsync(_uid); if (f != null) { _friends = f; foreach (var u in f) clb.Items.Add(u.Username ?? u.Email); } } catch { } }
        private async void Ok(object s, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || clb.CheckedIndices.Count == 0) return;
            List<string> uids = new List<string>(); foreach (int i in clb.CheckedIndices) uids.Add(_friends[i].Uid);
            try { btnOk.Text = "..."; await _db.CreateGroupAsync(txtName.Text.Trim(), _uid, uids); DialogResult = DialogResult.OK; Close(); } catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}