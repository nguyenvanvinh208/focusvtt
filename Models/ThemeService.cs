using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using Do_an.Models;
using System.Windows.Forms;

namespace Do_an.Services
{
    public class ThemeService
    {
        private const string DATA_URL = "https://raw.githubusercontent.com/nguyenvanvinh208/FocusVTT-Data/main/themes2.json";

        public async Task<List<ThemeInfo>> GetThemesAsync()
        {
            List<ThemeInfo> list = new List<ThemeInfo>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "FocusVTT-App");

                    string jsonContent = await client.GetStringAsync(DATA_URL);
                    if (string.IsNullOrWhiteSpace(jsonContent))
                    {
                        return list;
                    }

                    list = JsonConvert.DeserializeObject<List<ThemeInfo>>(jsonContent);

                    foreach (var theme in list)
                    {
                        if (!string.IsNullOrEmpty(theme.ThumbnailUrl))
                        {
                            try
                            {
                                var imgBytes = await client.GetByteArrayAsync(theme.ThumbnailUrl);
                                using (var ms = new System.IO.MemoryStream(imgBytes))
                                {
                                    theme.ThumbnailImg = Image.FromStream(ms);
                                }
                            }
                            catch
                            {
                                theme.ThumbnailImg = new Bitmap(200, 120);
                                using (Graphics g = Graphics.FromImage(theme.ThumbnailImg)) g.Clear(Color.Gray);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải Theme từ Server:\n" + ex.Message);
            }
            return list;
        }
    }
}