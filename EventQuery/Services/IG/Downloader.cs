using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace EventQuery.Services.IG
{
    public class Downloader
    {
        private string imageUrl;
        private Bitmap bitmap;
        public Downloader(string imageUrl)
        {
            this.imageUrl = imageUrl;
        }
        public Task Download(string filename, ImageFormat format)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    WebClient client = new WebClient();
                    Stream stream = client.OpenRead(imageUrl);
                    bitmap = new Bitmap(stream);
                    stream.Flush();
                    stream.Close();


                    if (bitmap != null)
                    {
                        bitmap.Save(filename, format);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            });
            return task;           
        }
        public Bitmap GetImage()
        {
            return bitmap;
        }
    }
}