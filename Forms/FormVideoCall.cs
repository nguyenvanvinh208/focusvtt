using Do_an.Firebase;
using Do_an.Models;
using Do_an.Services;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Do_an.Forms
{
    public partial class FormVideoCall : Form
    {
        private System.Windows.Forms.Timer _animTimer;
        private System.Windows.Forms.Timer _statusCheckTimer;
        private System.Windows.Forms.Timer _callDurationTimer;
        private int _callSeconds = 0;

        private int _rippleSize = 0;
        private bool _isIncoming, _isGroup;
        private string _partnerUid, _myUid;

        private FirebaseDatabaseService _dbService = new FirebaseDatabaseService();
        private static readonly HttpClient Http = new HttpClient();
        private const string FirebaseBaseUrl = FirebaseConfig.DatabaseUrl;

        private VoiceService _voiceService;

        public FormVideoCall(string name, string partnerUid, string myUid, bool isIncoming, bool isGroup, string type)
        {
            InitializeComponent();
            _partnerUid = partnerUid; _myUid = myUid; _isIncoming = isIncoming; _isGroup = isGroup;

            lblName.Text = name;
            lblStatus.Text = isIncoming ? "Cuộc gọi đến..." : (type + " Call...");

            LoadAvatar();
            if (isIncoming) SetupIncoming(); else SetupOutgoing();

            _animTimer = new System.Windows.Forms.Timer { Interval = 50 };
            _animTimer.Tick += (s, e) => { _rippleSize += 2; if (_rippleSize > 40) _rippleSize = 0; Invalidate(); };
            _animTimer.Start();

            _statusCheckTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            _statusCheckTimer.Tick += async (s, e) => await CheckStatus();
            _statusCheckTimer.Start();

            _callDurationTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            _callDurationTimer.Tick += (s, e) => {
                _callSeconds++;
                TimeSpan time = TimeSpan.FromSeconds(_callSeconds);
                lblStatus.Text = time.ToString(@"mm\:ss");
            };

            btnEnd.Click += async (s, e) => await EndCall();
        }

        private void LoadAvatar()
        {
            try
            {
                if (_isGroup) { picAvatar.BackColor = Color.Coral; return; }
                string p = System.IO.Path.Combine(Application.StartupPath, "UserAvatars", _partnerUid + ".jpg");
                if (System.IO.File.Exists(p)) picAvatar.Image = Image.FromFile(p); else picAvatar.BackColor = Color.Gray;
            }
            catch { picAvatar.BackColor = Color.Gray; }
        }

        private void SetupOutgoing()
        {
            lblStatus.Text = _isGroup ? "Đang gọi nhóm..." : "Đang đổ chuông...";
            btnAccept.Visible = false;
            btnEnd.Location = new Point(150, 480);
        }

        private void SetupIncoming()
        {
            lblStatus.Text = "Cuộc gọi đến...";
            btnAccept.Visible = true;
            btnEnd.Location = new Point(80, 480);
            btnAccept.Location = new Point(220, 480);

            btnAccept.Click += async (s, e) => {
                _animTimer.Stop();
                StartCallUI();
                btnAccept.Visible = false;
                btnEnd.Location = new Point(150, 480);
                await _dbService.UpdateCallStatusAsync(_myUid, "Accepted");
            };
        }

        private void StartCallUI()
        {
            lblStatus.ForeColor = Color.Lime;
            lblStatus.Text = "00:00";
            _animTimer.Stop();
            if (!_callDurationTimer.Enabled) _callDurationTimer.Start();
            StartVoiceChat();
        }

        private async void StartVoiceChat()
        {
            try
            {
                _voiceService = new VoiceService();
                string partnerIP = "";

                if (_isGroup) return;

                var partnerUser = await _dbService.GetUserAsync(_partnerUid);

                if (partnerUser != null && !string.IsNullOrEmpty(partnerUser.LocalIP))
                {
                    partnerIP = partnerUser.LocalIP;
                }

                if (string.IsNullOrEmpty(partnerIP) || partnerIP.StartsWith("127."))
                {
                    MessageBox.Show($"Lỗi: IP của người kia không hợp lệ ({partnerIP}).\nBảo họ Đăng xuất/Đăng nhập lại để cập nhật IP Wifi!");
                    return;
                }

                _voiceService.Start(partnerIP);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi động Voice: " + ex.Message);
            }
        }

        private async Task CheckStatus()
        {
            if (!_isIncoming)
            { // NGƯỜI GỌI
                if (_isGroup)
                {
                    if (_animTimer.Enabled) { await Task.Delay(2000); StartCallUI(); }
                    return;
                }
                try
                {
                    string url = $"{FirebaseBaseUrl}/CallRequests/{_partnerUid}.json?t={DateTime.Now.Ticks}";
                    string json = await Http.GetStringAsync(url);
                    if (!string.IsNullOrEmpty(json) && json != "null")
                    {
                        var call = JsonConvert.DeserializeObject<CallInfo>(json);
                        if (call.Status == "Accepted")
                        {
                            if (_voiceService == null) StartCallUI();
                        }
                        else if (call.Status == "Rejected") { MessageBox.Show("Người dùng bận."); Close(); }
                    }
                    else { Close(); }
                }
                catch { }
            }
            else
            { // NGƯỜI NHẬN
                try
                {
                    string url = $"{FirebaseBaseUrl}/CallRequests/{_myUid}.json?t={DateTime.Now.Ticks}";
                    string json = await Http.GetStringAsync(url);
                    if (string.IsNullOrEmpty(json) || json == "null")
                    {
                        MessageBox.Show("Cuộc gọi đã kết thúc.");
                        Close();
                    }
                }
                catch { }
            }
        }

        private async Task EndCall()
        {
            _statusCheckTimer.Stop();
            _callDurationTimer.Stop();
            if (_voiceService != null) _voiceService.Stop();
            if (_isIncoming) await _dbService.EndCallAsync(_myUid);
            else if (!_isGroup) await _dbService.EndCallAsync(_partnerUid);
            Close();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_animTimer != null && _animTimer.Enabled)
            {
                using (Pen p = new Pen(Color.FromArgb(100 - _rippleSize * 2, 0, 132, 255), 2))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.DrawEllipse(p, picAvatar.Left - _rippleSize, picAvatar.Top - _rippleSize, picAvatar.Width + _rippleSize * 2, picAvatar.Height + _rippleSize * 2);
                }
            }
        }
    }
}


