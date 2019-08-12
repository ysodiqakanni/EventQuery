using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventQuery.Models.IG
{
    public class JsonParser
    {
        public static List<UserInformation> ExtractUserData(JsonDataRepresentation jDR)
        {
            var list = new List<UserInformation>();
            if (jDR == null)
            {
                return null;
            }

            var entryData = jDR.entry_data;
            if (entryData == null)
                return null;

            var profPage = entryData.ProfilePage;
            if (profPage == null || !profPage.Any())
                return null;

            foreach (var im in profPage)
            {
                var graphQl = im.graphql;
                if (graphQl == null)
                    return null;

                var user = graphQl.user;
                if (user == null)
                    return null;

                var mediaTimeLine = user.edge_owner_to_timeline_media;
                if (mediaTimeLine == null)
                    return null;

                var edges = mediaTimeLine.edges;
                if (edges == null || !edges.Any())
                    return null;

                foreach (var edge in edges)
                {
                    var node = edge.node;
                    if (node == null)
                        continue;

                    var userData = new UserInformation
                    {
                        ImageUrl = node.display_url,
                        CreatedOn = UnixTimeStampToDateTime(node.taken_at_timestamp),
                        DateGenerated = DateTime.UtcNow
                    };

                    // get only media for today
                    if (userData.CreatedOn.Date != DateTime.UtcNow.Date) continue;

                    var caption = node.edge_media_to_caption;
                    if (caption != null)
                    {
                        var text = "";
                        var textEdges = caption.edges;
                        if (textEdges != null && textEdges.Any())
                        {
                            foreach (var tEdge in textEdges)
                            {
                                text += $"{tEdge.node?.text}. ";
                            }
                        }
                        userData.Title = text;
                    }
                    list.Add(userData);
                }
            }

            return list;
        }

        public static List<UserInformation> ExtractTagData(TagInformation jDR)
        {
            var list = new List<UserInformation>();
            if (jDR == null)
            {
                return null;
            }

            var entryData = jDR.entry_data;
            if (entryData == null)
                return null;

            var profPage = entryData.TagPage;
            if (profPage == null || !profPage.Any())
                return null;

            foreach (var im in profPage)
            {
                var graphQl = im.graphql;
                if (graphQl == null)
                    return null;

                var user = graphQl.hashtag;
                if (user == null)
                    return null;

                var mediaTimeLine = user.edge_hashtag_to_media;
                if (mediaTimeLine == null)
                    return null;

                var edges = mediaTimeLine.edges;
                if (edges == null || !edges.Any())
                    return null;

                foreach (var edge in edges)
                {
                    var node = edge.node;
                    if (node == null)
                        continue;

                    var userData = new UserInformation
                    {
                        ImageUrl = node.display_url,
                        CreatedOn = UnixTimeStampToDateTime(node.taken_at_timestamp),
                        DateGenerated = DateTime.UtcNow
                    };

                    var caption = node.edge_media_to_caption;
                    if (caption != null)
                    {
                        var text = "";
                        var textEdges = caption.edges;
                        if (textEdges != null && textEdges.Any())
                        {
                            foreach (var tEdge in textEdges)
                            {
                                text += $"{tEdge.node?.text}. ";
                            }
                        }
                        userData.Title = text;
                    }
                    list.Add(userData);
                }
            }

            return list;
        }

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

    }
}