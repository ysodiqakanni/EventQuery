using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventQuery.Models.IG
{
    public class TagInformation
    {
        public TagEntryData entry_data { get; set; }
    }

    public class TagEntryData
    {
        public List<TagPage> TagPage { get; set; }
    }

    public class TagPage
    {
        public TagGraphQl graphql { get; set; }
    }

    public class TagGraphQl
    {
        public HashTag hashtag { get; set; }
    }

    public class HashTag
    {
        public EdgeHashTagToMedia edge_hashtag_to_media { get; set; }
    }

    public class EdgeHashTagToMedia
    {
        public List<TagEdge> edges { get; set; }
    }

    public class TagEdge
    {
        public TagNode node { get; set; }
    }

    public class TagNode
    {
        public string display_url { get; set; }

        public long taken_at_timestamp { get; set; }

        public TagEdgeMediaToCaption edge_media_to_caption { get; set; }
    }

    public class TagEdgeMediaToCaption
    {
        public List<TagTextEdge> edges { get; set; }
    }

    public class TagTextEdge
    {
        public TagTextNode node { get; set; }
    }

    public class TagTextNode
    {
        public string text { get; set; }
    }
}