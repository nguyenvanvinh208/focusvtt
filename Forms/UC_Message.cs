using Do_an.Firebase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Do_an.Forms
{
    public partial class UC_Message : UserControl
    {
        public event Action OnMenuClicked;
        private readonly FirebaseDatabaseService _dbService = new FirebaseDatabaseService();
        private static readonly HttpClient Http = new HttpClient();

        private ContextMenuStrip _emojiMenu;
        private ContextMenuStrip _friendMenu;

        // --- THAY ĐỔI MÀU BONG BÓNG CHAT (THEME VINTAGE) ---
        // Tin nhắn của mình: Màu gỗ tối/da bò
        private readonly Color clrMineMsg = Color.FromArgb(101, 67, 33);
        // Tin nhắn người khác: Màu giấy cũ/Parchment
        private readonly Color clrOtherMsg = Color.FromArgb(218, 200, 162);
        // ---------------------------------------------------

        private readonly Dictionary<string, List<ChatMessage>> _conversations = new Dictionary<string, List<ChatMessage>>();
        private readonly Dictionary<string, HashSet<string>> _seenMessageIds = new Dictionary<string, HashSet<string>>();
        private readonly Dictionary<string, string> _idToName = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _idToUid = new Dictionary<string, string>();
        private readonly HashSet<string> _groupIds = new HashSet<string>();
        private readonly Dictionary<string, Panel> _chatPanels = new Dictionary<string, Panel>();
        private readonly string _avatarFolder = Path.Combine(Application.StartupPath, "UserAvatars");
        private const string FirebaseBaseUrl = FirebaseConfig.DatabaseUrl;
        private const long MaxFileSizeBytes = 2L * 1024 * 1024;

        private string _currentChatId = null;
        private bool _isGroupChat = false;
        public string CurrentUserUid { get; private set; }
        public string CurrentUserEmail { get; private set; }
        private bool _initialized = false;
        private System.Windows.Forms.Timer _pollTimer;

        private class FirebaseMessage { public string text, timestamp, peerUid, peerEmail, imageBase64, fileName, fileBase64, senderName; public bool isMine; public long fileSize; }
        private class ChatMessage { public string Text; public bool IsMine; public string ImageBase64, FileName, FileBase64; public long FileSize; public string SenderName; public string SenderUid; }
        private class FileBubbleTag { public string FileName, FileBase64; public long FileSize; }
        public UC_Message()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            this.BackColor = Color.White; // Nền backup
            this.Load += UC_Message_Load;

            btnSend.Click += btnSend_Click;
            btnEmoji.Click += btnEmoji_Click;
            btnImage.Click += btnImage_Click;
            btnAttach.Click += btnAttach_Click;
            if (btnMenu != null) btnMenu.Click += (s, e) => OnMenuClicked?.Invoke();

            // Giữ lại Voice Call
            if (btnVoiceCall != null) btnVoiceCall.Click += async (s, e) => await MakeCall("Voice");

            // Đã xóa phần btnVideoCall.Click...

            btnAddFriend.Click += async (s, e) => {
                using (var frm = new FormCustomInput("THÊM BẠN MỚI", "Nhập Email/Username..."))
                    if (frm.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(frm.ResultText))
                        try { await _dbService.SendFriendRequestAsync(CurrentUserUid, frm.ResultText.Trim()); MessageBox.Show("Đã gửi lời mời!"); } catch (Exception ex) { MessageBox.Show(ex.Message); }
            };

            btnAccept.Click += async (s, e) => {
                try
                {
                    var req = await _dbService.GetPendingRequestsAsync(CurrentUserUid);
                    if (req == null || req.Count == 0) { MessageBox.Show("Không có lời mời nào."); return; }
                    using (var frm = new FormRequestList(req))
                        if (frm.ShowDialog() == DialogResult.OK) { await _dbService.AcceptFriendRequestAsync(CurrentUserUid, frm.SelectedUid); MessageBox.Show("Đã kết bạn!"); await LoadChatList(); }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            };

            btnCreateGroup.Click += async (s, e) => {
                using (var frm = new FormCreateGroup(CurrentUserUid))
                    if (frm.ShowDialog() == DialogResult.OK) { MessageBox.Show("Tạo nhóm thành công!"); await LoadChatList(); }
            };

            if (btnMoreInfo != null) btnMoreInfo.Click += async (s, e) => {
                if (string.IsNullOrEmpty(_currentChatId)) return;
                string msg = _isGroupChat ? "Rời nhóm?" : "Xóa chat?";
                if (MessageBox.Show(msg, "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        if (_isGroupChat) await _dbService.LeaveGroupAsync(_currentChatId, CurrentUserUid);
                        else if (_idToUid.TryGetValue(_currentChatId, out string uid)) await _dbService.DeleteConversationAsync(CurrentUserUid, uid);
                        flChatList.Controls.RemoveByKey(_currentChatId); ResetRightPanel();
                    }
                    catch { }
                }
            };

            txtSearch.TextChanged += (s, e) => FilterChatList(txtSearch.Text);
            flMessages.SizeChanged += (s, e) => RelayoutAllRows();
            BuildEmojiMenu();
        }
        public void InitializeCurrentUser(string uid, string email) { CurrentUserUid = uid; CurrentUserEmail = email; _initialized = true; InitMessagePolling(); }
        private bool EnsureInitialized() => _initialized && !string.IsNullOrEmpty(CurrentUserUid);
        private async void UC_Message_Load(object sender, EventArgs e) => await LoadChatList();

        private void InitMessagePolling()
        {
            if (_pollTimer != null) { _pollTimer.Stop(); _pollTimer.Dispose(); }

            // [ĐÃ CHỈNH SỬA] Tăng Interval lên 3000 (3 giây)
            // Bạn có thể sửa thành 5000 (5 giây) nếu muốn lâu hơn nữa
            _pollTimer = new System.Windows.Forms.Timer { Interval = 3000 };

            _pollTimer.Tick += async (s, e) => {
                await CheckIncomingMessages();
                await UpdateFriendStatusUI();
                await _dbService.UpdateUserActivity(CurrentUserUid, true);
            };
            _pollTimer.Start();
        }
        private async Task UpdateFriendStatusUI()
        {
            try
            {
                var friends = await _dbService.GetFriendsAsync(CurrentUserUid);
                if (friends == null) return;
                foreach (var u in friends)
                {
                    if (string.IsNullOrEmpty(u.Email)) continue;

                    // Sync Avatar
                    if (!string.IsNullOrEmpty(u.AvatarBase64))
                    {
                        string localPath = Path.Combine(_avatarFolder, u.Uid + ".jpg");
                        try
                        {
                            if (!File.Exists(localPath))
                            {
                                byte[] b = Convert.FromBase64String(u.AvatarBase64);
                                File.WriteAllBytes(localPath, b);
                                if (_chatPanels.TryGetValue(u.Email, out Panel pp))
                                    foreach (Control c in pp.Controls) if (c is CircularPictureBox pc) using (var ms = new MemoryStream(b)) pc.Image = Image.FromStream(ms);
                            }
                        }
                        catch { }
                    }

                    if (_chatPanels.TryGetValue(u.Email, out Panel p))
                    {
                        bool isOnline = u.IsOnline;
                        if (isOnline && !string.IsNullOrEmpty(u.LastActive))
                        {
                            if (DateTime.TryParse(u.LastActive, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime t))
                                if ((DateTime.UtcNow - t).TotalMinutes > 2) isOnline = false;
                        }
                        foreach (Control c in p.Controls)
                        {
                            if (c is Label lbl && c.Location.Y > 30) { lbl.Text = isOnline ? "Online" : "Offline"; lbl.ForeColor = isOnline ? Color.LimeGreen : Color.Gray; }
                            if (c is Panel dot && c.Height < 20) dot.Visible = isOnline;
                        }
                        if (_currentChatId == u.Email && !_isGroupChat) lblChatStatus.Text = isOnline ? "Đang hoạt động" : "Truy cập " + GetTimeAgo(u.LastActive);
                    }
                }
            }
            catch { }
        }

        private string GetTimeAgo(string s) { if (string.IsNullOrEmpty(s)) return "gần đây"; if (DateTime.TryParse(s, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime t)) { var d = DateTime.UtcNow - t; if (d.TotalMinutes < 1) return "vừa xong"; if (d.TotalMinutes < 60) return (int)d.TotalMinutes + " phút trước"; return (int)d.TotalDays + " ngày trước"; } return "gần đây"; }

    }
}
