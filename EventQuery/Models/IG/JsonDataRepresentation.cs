using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventQuery.Models.IG
{
    public class JsonDataRepresentation
    {
        public EntryData entry_data { get; set; }
    }

    public class EntryData
    {
        public List<ProfilePage>  ProfilePage {get;set;}
    }

    public class ProfilePage
    {
        public GraphQl graphql { get; set; }
    }

    public class GraphQl
    {
        public User user { get; set; }
    }

    public class User
    {
        public EdgeOwnerToTimelineMedia edge_owner_to_timeline_media { get; set; }
    }

    public class EdgeOwnerToTimelineMedia
    {
        public List<Edge> edges { get; set; }
    }

    public class Edge
    {
        public Node node { get; set; }
    }

    public class Node
    {
        public EdgeTextNode edge_media_to_caption { get; set; }

        public long taken_at_timestamp { get; set; }

        public string display_url { get; set; }
    }

    public class EdgeMediaToCaption
    {
        public List<Edge> edges { get; set; }
    }

    public class EdgeTextNode
    {
        public List<TextEdge> edges { get; set; }
    }

    public class TextEdge
    {
        public TextNode node { get; set; }
    }

    public class TextNode
    {
        public string text { get; set; }
    }
}