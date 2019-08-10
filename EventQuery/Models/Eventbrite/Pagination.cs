using Newtonsoft.Json;

namespace EventQuery.Models.Eventbrite
{
    public class Pagination
    {
        [JsonProperty(PropertyName="page_count")]
        public long pageCount{get;set;}
    }
}