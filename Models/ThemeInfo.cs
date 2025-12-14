using System.Drawing;

namespace Do_an.Models
{
    public class ThemeInfo
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string ThumbnailUrl { get; set; }
        public string VideoUrl { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public Image ThumbnailImg { get; set; }
    }
}