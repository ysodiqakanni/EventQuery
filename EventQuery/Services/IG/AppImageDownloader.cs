
using EventQuery.Data;
using EventQuery.Data.IG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EventQuery.Services.IG
{
    public class AppImageDownloader
    {
        private static Task BuildGenerallyOld(string path)
        {
            var task = Task.Run(async () =>
            {
                var usernames = new List<string> { "leomessi", "beyonce", "nasa" };
                var tags = new List<string> { "love", "makeupartist", "football", "cakes"};

                var usernameUrl = "https://www.instagram.com/";
                var tagUrl = "https://www.instagram.com/explore/tags/";

                var list2 = await LoadHtmlPage.LoadUserDataFromPage(tags, tagUrl, DataType.HashTag);
                var list = await LoadHtmlPage.LoadUserDataFromPage(usernames, usernameUrl, DataType.User);
                if (list != null && list.Any())
                {                    
                    var infoList = await DownloadImages.LoadUserImages(list, path); 

                    foreach (var userInfo in infoList)
                    {
                        userInfo.Id = Guid.NewGuid().ToString();
                        userInfo.RetrievedUsing = "Username"; 
                    }
                    IGDatabaseHelper.SaveUserInfoToDb(infoList);
                }

                if (list2 != null && list2.Any())
                { 
                    var infoList = await DownloadImages.LoadUserImages(list2, path);
                    foreach (var userInfo in infoList)
                    {
                        userInfo.Id = Guid.NewGuid().ToString();
                        userInfo.RetrievedUsing = "Hashtag"; 
                    }
                    IGDatabaseHelper.SaveUserInfoToDb(infoList);
                }

            });
            return task;
        }
        public static Task BuildWithUsername(string path, string username)
        {
            var task = Task.Run(async () =>
            {
                
                var usernames = username.Split(' ').ToList();   

                var usernameUrl = "https://www.instagram.com/";  
                var list = await LoadHtmlPage.LoadUserDataFromPage(usernames, usernameUrl, DataType.User);
                if (list != null && list.Any())
                {
                    var infoList = await DownloadImages.LoadUserImages(list, path);

                    foreach (var userInfo in infoList)
                    {
                        userInfo.Id = Guid.NewGuid().ToString();
                        userInfo.RetrievedUsing = "Username";
                    }
                    IGDatabaseHelper.SaveUserInfoToDb(infoList);
                } 
            });
            return task;
        }
        public static Task BuildWithTags(string path, string tag)
        {
            var task = Task.Run(async () =>
            { 
                var tags = tag.Split(',').ToList();  
                 
                var tagUrl = "https://www.instagram.com/explore/tags/";

                var list2 = await LoadHtmlPage.LoadUserDataFromPage(tags, tagUrl, DataType.HashTag); 
                
                if (list2 != null && list2.Any())
                {
                    var infoList = await DownloadImages.LoadUserImages(list2, path);
                    foreach (var userInfo in infoList)
                    {
                        userInfo.Id = Guid.NewGuid().ToString();
                        userInfo.RetrievedUsing = "Hashtag";
                    }
                    IGDatabaseHelper.SaveUserInfoToDb(infoList);
                }

            });
            return task;
        }
    }
}