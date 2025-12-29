using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;

namespace Do_an.Forms
{
    public partial class UC_Home : UserControl
    {
        private struct SlideData
        {
            public Image Img;
            public string Title;
            public string Desc;
            public string FeatureID;
        }

        private List<SlideData> _slides = new List<SlideData>();
        private int _currentIndex = 0;

        public event Action<string> OnBannerClicked;
        public event Action OnMenuClicked;

        public UC_Home()
        {
            InitializeComponent();
            LoadSlideData();
        }

        private void UC_Home_Load(object sender, EventArgs e)
        {
            ShowCurrentSlide();
            MakeRound(btnPrev);
            MakeRound(btnNext);

            // Tìm nút menu (vẫn hoạt động tốt dù menu đã nằm trong picturebox)
            Control menu = this.Controls.Find("lblMenu", true).Length > 0 ? this.Controls.Find("lblMenu", true)[0] : null;
            if (menu != null) menu.Click += (s, ev) => OnMenuClicked?.Invoke();

            picSlider.Click += (s, ev) => OnBannerClicked?.Invoke(_slides[_currentIndex].FeatureID);
        }

        private void LoadSlideData()
        {
            // Đảm bảo bạn có Resources tương ứng (schedule, rank, message)
            // Nếu chưa có, hãy thêm ảnh vào Project -> Properties -> Resources
            try
            {
                _slides.Add(new SlideData
                {
                    Title = "LÊN LỊCH",
                    Desc = "Quản lý thời gian hiệu quả",
                    FeatureID = "SCHEDULE",
                    Img = ByteToImg(Properties.Resources.schedule)
                });

                _slides.Add(new SlideData
                {
                    Title = "XẾP HẠNG",
                    Desc = "Thi đua cùng bạn bè",
                    FeatureID = "RANKING",
                    Img = ByteToImg(Properties.Resources.rank)
                });

                _slides.Add(new SlideData
                {
                    Title = "NHẮN TIN",
                    Desc = "Kết nối cộng đồng",
                    FeatureID = "CHAT",
                    Img = ByteToImg(Properties.Resources.message)
                });
            }
            catch (Exception ex)
            {
                // Xử lý nếu thiếu resource để không crash
                MessageBox.Show("Vui lòng kiểm tra lại file Resources ảnh: " + ex.Message);
            }
        }

        private Image ByteToImg(object resource)
        {
            if (resource is Image) return (Image)resource;
            if (resource is byte[])
            {
                byte[] imageBytes = (byte[])resource;
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    return Image.FromStream(ms);
                }
            }
            return null;
        }

        private void ShowCurrentSlide()
        {
            if (_slides.Count == 0) return;
            var slide = _slides[_currentIndex];

            picSlider.Image = slide.Img;
            lblFeatureTitle.Text = slide.Title;
            lblFeatureDesc.Text = slide.Desc;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            _currentIndex--;
            if (_currentIndex < 0) _currentIndex = _slides.Count - 1;
            ShowCurrentSlide();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _currentIndex++;
            if (_currentIndex >= _slides.Count) _currentIndex = _slides.Count - 1;
            ShowCurrentSlide();
        }

        private void MakeRound(Control c)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, c.Width, c.Height);
            c.Region = new Region(gp);
        }
    }
}