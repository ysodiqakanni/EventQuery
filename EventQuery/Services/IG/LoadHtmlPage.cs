using EventQuery.Models.IG;
using HtmlAgilityPack; 
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace EventQuery.Services.IG
{
    public class LoadHtmlPage
    {

        public static Task<Dictionary<string, List<UserInformation>>> LoadUserDataFromPage(List<string> usernames, string rootUrl, DataType dataType)
        {
            var task = Task.Run(async () =>
            {
                var dict = new Dictionary<string, List<UserInformation>>();
                if (usernames == null || !usernames.Any())
                    return dict;

                foreach (var username in usernames)
                {

                    var list = new List<UserInformation>();
                    string url = $"{rootUrl}{username}/";
                    var data = await LoadPageAndGetScripts(url);
                    if (data == null || !data.Any())
                        continue;

                    foreach (var dataStr in data)
                    {
                        List<UserInformation> userData = null;

                        if(dataType == DataType.User)
                        {
                            var jsonDataRepresentation = JsonConvert.DeserializeObject<JsonDataRepresentation>(dataStr);
                            userData = JsonParser.ExtractUserData(jsonDataRepresentation);
                        }
                        else
                        {
                            var dataRepresentation = JsonConvert.DeserializeObject<TagInformation>(dataStr);
                            userData = JsonParser.ExtractTagData(dataRepresentation);
                        }
                        
                        if (userData != null)
                        { 
                            list.AddRange(userData);
                        }
                    }
                    dict.Add(username, list);
                }
                
                return dict;
            });
            return task;
        }
             
        private static Task<List<string>> LoadPageAndGetScripts(string url)
        {
            var task = Task.Run(() =>
           {
               var list = new List<string>();
               if (string.IsNullOrWhiteSpace(url))
                   return list;

               var web = new HtmlWeb();
               var doc = web.Load(url);

               if (doc == null)
                   return list;

               // InnerText 
               var scripts = doc.DocumentNode.Descendants("script");
               if (scripts != null && scripts.Any())
               {
                   foreach(var scrpt in scripts)
                   {
                       var innerText = scrpt.InnerText;
                       if (innerText.Contains("window._sharedData = {\"config\""))
                       {
                           innerText = innerText.Substring(innerText.IndexOf("window._sharedData = ") + 21);
                           innerText = innerText.TrimEnd(new[] { ';', ' ' });
                           list.Add(innerText);
                       }                       
                   }
               }
               return list;
           });
            return task;
        }

        public static Task<List<string>> LoadUserUrlsFromPages(string url)
        {
            var task = Task.Run(async () =>
            {
                var scripts = await LoadPageAndGetScripts(url);
                var probableUrl = new List<string>();
                var list = new List<string>();
                if (scripts != null && scripts.Any())
                {
                    foreach (var rawString in scripts)
                    {
                        var reg = new Regex("\".*?\"");
                        var matches = reg.Matches(rawString);
                        foreach (var item in matches)
                        {
                            probableUrl.Add(item.ToString());
                        }
                    }


                    var linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    foreach (var probable in probableUrl)
                    {
                        if (probable.Contains("https://scontent"))
                        {
                            list.Add(probable);
                        }
                    }
                    /*foreach (Match m in linkParser.Matches(rawString))
                    {
                        list.Add(m.Value);
                    }*/

                }
                return list;
            });
            return task;
        }

        public static Task<Dictionary<string, List<string>>> LoadUrlsForUser(List<string> usernames, string rootUrl)
        {
            var task = Task.Run(async () =>
            {
                var dictionary = new Dictionary<string, List<string>>();
                if (usernames != null && usernames.Any())
                {
                    foreach(var username in usernames)
                    {
                        string url = $"{rootUrl}{username}/";
                        var list = await LoadUserUrlsFromPages(url);
                        dictionary.Add(username, list);
                    }
                }
                return dictionary;
            });
            return task;           
        }
    }
}