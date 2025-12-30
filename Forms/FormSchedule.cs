using Do_an.Firebase;
using Do_an.Models;

namespace Do_an.Forms
{
    public partial class FormSchedule : Form
    {
        private User _currentUser;
        private List<FlowLayoutPanel> _dayPanels = new List<FlowLayoutPanel>();
        private System.Windows.Forms.Timer _updateTimer;
        private DateTime _currentWeekMonday;

        private FirebaseDatabaseService _dbService;

        public event Action OnMenuClicked;

        public FormSchedule(User user)
        {
            InitializeComponent();
            _currentUser = user;


            _dbService = new FirebaseDatabaseService();


            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = Color.FromArgb(15, 10, 30);
            try { this.BackgroundImage = ByteToImg(Properties.Resources.universe); } catch { }

            CalculateCurrentWeek();
            SetupCustomGridLogic();


            _updateTimer = new System.Windows.Forms.Timer();
            _updateTimer.Interval = 30000;
            _updateTimer.Tick += (s, e) => RefreshTaskStatus();
            _updateTimer.Start();

            _ = LoadWeeklyScheduleAsync();
        }


        private async Task LoadWeeklyScheduleAsync()
        {
            foreach (var panel in _dayPanels)
            {
                for (int i = panel.Controls.Count - 1; i >= 0; i--)
                {
                    if (panel.Controls[i] is ScheduleCard)
                    {
                        panel.Controls.RemoveAt(i);
                    }
                }
            }

            // 2. Duyệt qua 7 ngày trong tuần
            for (int i = 0; i < 7; i++)
            {
                DateTime targetDate = _currentWeekMonday.AddDays(i);

                var tasks = await _dbService.GetTasksByDateAsync(_currentUser.Uid, targetDate);

                if (tasks != null)
                {
                    foreach (var task in tasks)
                    {
                     
                        AddTaskCard(i, task.Id, task.Name, task.Start, task.End, task.IsDone);
                    }
                }
            }
        }

        private void LblMenu_Click(object sender, EventArgs e)
        {
            OnMenuClicked?.Invoke();
        }

        private async void DayPanel_Click(object sender, EventArgs e)
        {
            FlowLayoutPanel pnl = sender as FlowLayoutPanel;
            int dayIndex = (int)pnl.Tag;
            DateTime targetDate = _currentWeekMonday.AddDays(dayIndex);

            if (targetDate.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Không thể đặt lịch cho ngày trong quá khứ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<TaskInfo> existingTasks = GetTasksForDay(dayIndex, targetDate);
            FormAddTask frm = new FormAddTask(targetDate, existingTasks);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                TaskInfo newTask = new TaskInfo
                {
                    Name = frm.TaskNameResult,
                    Start = frm.StartTimeResult,
                    End = frm.EndTimeResult,
                    IsDone = false
                };

                try
                {
                    await _dbService.AddTaskAsync(_currentUser.Uid, targetDate, newTask);
                    AddTaskCard(dayIndex, newTask.Id, newTask.Name, newTask.Start, newTask.End, newTask.IsDone);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message);
                }
            }
        }

        private async void Task_Click(ScheduleCard card)
        {
            card.UpdateStatus();

            if (card.CurrentStatus == CardStatus.Running)
            {
                this.FindForm().Hide();

                TaskInfo info = new TaskInfo { Name = card.TaskName, Start = card.StartTime, End = card.EndTime };

                FormFocusMode focus = new FormFocusMode(info, _currentUser);

                if (focus.ShowDialog() == DialogResult.OK)
                {
                    card.IsDone = true;
                    card.UpdateStatus();
                    card.Invalidate();

                    if (!string.IsNullOrEmpty(card.TaskId))
                    {
                        DateTime taskDate = card.StartTime.Date;
                        try
                        {
                            await _dbService.UpdateTaskStatusAsync(_currentUser.Uid, taskDate, card.TaskId, true);
                        }
                        catch { }
                    }
                }
                this.FindForm().Show();
            }
            else if (card.CurrentStatus == CardStatus.Future)
                MessageBox.Show($"Chưa tới giờ!\nBắt đầu: {card.StartTime:HH:mm}");
            else if (card.CurrentStatus == CardStatus.Missed)
                MessageBox.Show("Đã quá hạn!");
            else if (card.CurrentStatus == CardStatus.Done)
                MessageBox.Show("Đã hoàn thành công việc này!");
        }


        private void CalculateCurrentWeek()
        {
            DateTime today = DateTime.Today;
            int delta = DayOfWeek.Monday - today.DayOfWeek;
            if (delta > 0) delta -= 7;
            _currentWeekMonday = today.AddDays(delta);
        }

        private Image ByteToImg(object resource)
        {
            if (resource == null) return null;
            if (resource is Image) return (Image)resource;
            if (resource is byte[]) return Image.FromStream(new MemoryStream((byte[])resource));
            return null;
        }

        private List<TaskInfo> GetTasksForDay(int dayIndex, DateTime targetDate)
        {
            List<TaskInfo> list = new List<TaskInfo>();
            if (dayIndex < 0 || dayIndex >= _dayPanels.Count) return list;
            foreach (Control c in _dayPanels[dayIndex].Controls)
            {
                if (c is ScheduleCard card)
                {
                    DateTime s = new DateTime(targetDate.Year, targetDate.Month, targetDate.Day, card.StartTime.Hour, card.StartTime.Minute, 0);
                    DateTime end = new DateTime(targetDate.Year, targetDate.Month, targetDate.Day, card.EndTime.Hour, card.EndTime.Minute, 0);
                    list.Add(new TaskInfo { Id = card.TaskId, Name = card.TaskName, Start = s, End = end, IsDone = card.IsDone });
                }
            }
            return list;
        }

        public void AddTaskCard(int dayIndex, string id, string taskName, DateTime startTime, DateTime endTime, bool isDone)
        {
            if (dayIndex < 0 || dayIndex >= 7) return;

            ScheduleCard card = new ScheduleCard();
            card.SetupData(id, taskName, startTime, endTime, isDone);

            int w = _dayPanels[dayIndex].ClientSize.Width - 4;
            card.Size = new Size(w, 35);
            card.Margin = new Padding(2, 0, 2, 3);
            card.Click += (s, e) => Task_Click(card);

            _dayPanels[dayIndex].Controls.Add(card);
        }

        private void RefreshTaskStatus()
        {
            foreach (var panel in _dayPanels)
                foreach (Control c in panel.Controls)
                    if (c is ScheduleCard card) card.Invalidate();
        }

        private void SetupCustomGridLogic()
        {
            _dayPanels.Clear();
            tableLayout.Controls.Clear();
            string[] days = { "T2", "T3", "T4", "T5", "T6", "T7", "CN" };

            for (int i = 0; i < 7; i++)
            {
                Label lblDay = new Label();
                lblDay.Text = days[i];
                lblDay.ForeColor = Color.White;
                lblDay.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                lblDay.TextAlign = ContentAlignment.MiddleCenter;
                lblDay.Dock = DockStyle.Fill;
                tableLayout.Controls.Add(lblDay, i, 0);

                FlowLayoutPanel pnlDay = new FlowLayoutPanel();
                pnlDay.Dock = DockStyle.Fill;
                pnlDay.FlowDirection = FlowDirection.TopDown;
                pnlDay.WrapContents = false;
                pnlDay.AutoScroll = true;
                pnlDay.BackColor = Color.Transparent;
                pnlDay.Tag = i;
                pnlDay.Click += DayPanel_Click;
                pnlDay.Padding = new Padding(0, 2, 0, 0);
                pnlDay.SizeChanged += (s, e) =>
                {
                    foreach (Control c in pnlDay.Controls) c.Width = pnlDay.ClientSize.Width - 4;
                    pnlDay.Invalidate();
                };
                pnlDay.Paint += PnlDay_Paint;

                _dayPanels.Add(pnlDay);
                tableLayout.Controls.Add(pnlDay, i, 1);
                tableLayout.SetRowSpan(pnlDay, 7);
            }
        }

        private void PnlDay_Paint(object sender, PaintEventArgs e)
        {
            FlowLayoutPanel pnl = sender as FlowLayoutPanel;
            Graphics g = e.Graphics;
            Color gridColor = Color.FromArgb(60, 100, 200, 255);
            using (Pen p = new Pen(gridColor, 1))
            {
                g.DrawLine(p, pnl.Width - 1, 0, pnl.Width - 1, pnl.Height);
                float cellHeight = (float)pnl.Height / 7.0f;
                for (int k = 1; k <= 7; k++)
                {
                    float y = k * cellHeight;
                    g.DrawLine(p, 0, y, pnl.Width, y);
                }
            }
        }
    }

    public enum CardStatus { Future, Running, Missed, Done }

    public class ScheduleCard : System.Windows.Forms.Control
    {
        public string TaskId;
        public string TaskName;
        public DateTime StartTime;
        public DateTime EndTime;
        public bool IsDone;
        public CardStatus CurrentStatus { get; private set; }

        public ScheduleCard()
        {
            DoubleBuffered = true;
            SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Cursor = Cursors.Hand;
            this.Height = 35;
        }

        public void SetupData(string id, string name, DateTime start, DateTime end, bool done)
        {
            TaskId = id;
            TaskName = name;
            StartTime = start;
            EndTime = end;
            IsDone = done;
            UpdateStatus();
            Invalidate();
        }

        public void UpdateStatus()
        {
            DateTime now = DateTime.Now;
            if (IsDone) CurrentStatus = CardStatus.Done;
            else if (now >= StartTime && now <= EndTime) CurrentStatus = CardStatus.Running;
            else if (now > EndTime) CurrentStatus = CardStatus.Missed;
            else CurrentStatus = CardStatus.Future;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            UpdateStatus();
            System.Drawing.Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            System.Drawing.Color border = System.Drawing.Color.Gray, fill = System.Drawing.Color.FromArgb(200, 20, 20, 30), text = System.Drawing.Color.White;

            switch (CurrentStatus)
            {
                case CardStatus.Done: border = System.Drawing.Color.SpringGreen; fill = System.Drawing.Color.FromArgb(100, 0, 50, 0); break;
                case CardStatus.Running: border = System.Drawing.Color.Gold; fill = System.Drawing.Color.FromArgb(200, 100, 80, 0); text = System.Drawing.Color.Yellow; break;
                case CardStatus.Missed: border = System.Drawing.Color.Red; text = System.Drawing.Color.Silver; break;
                case CardStatus.Future: border = System.Drawing.Color.Cyan; break;
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
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }
    }
}