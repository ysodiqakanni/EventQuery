using EventQuery.Models.IG; 
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EventQuery.Services.IG
{
    public class DownloadImages
    {
        public static Task<List<UserInformation>> LoadUserImages(Dictionary<string, List<UserInformation>> userList, string path)
        {
            var tsk = Task.Run(async () =>
            {
                var infoList = new List<UserInformation>();
                if (userList != null && userList.Any())
                {
                    foreach (var usr in userList)
                    {
                        var key = usr.Key;
                        if (usr.Value == null || !usr.Value.Any()) continue;
                        var urls = usr.Value;
                       

                        var newPath = Path.Combine(path, key);
                        if (!Directory.Exists(newPath))
                        {
                            Directory.CreateDirectory(newPath);
                        }
                        var infos = await PullImages(urls, newPath, key);
                        infoList.AddRange(infos);
                    }
                }
                return infoList;
            });
            return tsk;
        }

        public static Task<List<UserInformation>> PullImages(List<UserInformation> userInfo, string storePath, string key)
        {
            var task = Task.Run(async () =>
            {
                var infos = new List<UserInformation>();
                if(userInfo != null && userInfo.Any())
                {
                    foreach (var info in userInfo)
                    {
                        var newUrl = info.ImageUrl.Replace("\"", "");
                        var downloader = new Downloader(newUrl);
                        var guid = Guid.NewGuid().ToString();
                        var ext = GetExtension(newUrl);
                        var format = GetFormat(newUrl);

                        guid += ext;
                        var path = Path.Combine(storePath, guid);
                        var loadPath = $"~/images/{key}/{guid}";

                        info.ImageUrl = loadPath;
                        infos.Add(info);
                        await downloader.Download(path, format);
                    }
                }
                return infos;
            });
            return task;
        }

        private static ImageFormat GetFormat(string url)
        {
            var format = url.Contains(".png") ?
            ImageFormat.Png : url.Contains(".jpg") ?
            ImageFormat.Jpeg : url.Contains(".jpeg") ?
            ImageFormat.Jpeg : url.Contains(".gif") ?
            ImageFormat.Gif: ImageFormat.Png;

            return format;
        }

        private static string GetExtension(string url)
        {
            var ext = url.Contains(".png") ?
            ".png" : url.Contains(".jpg") ?
            ".jpg" : url.Contains(".jpeg") ?
            ".jpeg" : url.Contains(".gif") ? ".gif" : ".jpg";

            return ext;
        }
    }
}