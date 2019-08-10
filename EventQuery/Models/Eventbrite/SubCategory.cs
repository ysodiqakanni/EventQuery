using System.Threading;
using System.Net.Http;
using System;
using Newtonsoft.Json;

namespace EventQuery.Models.Eventbrite
{
   public class SubCategory
    {
       public int id { get; set; }
       public String name {get;set;}
        [JsonProperty(PropertyName = "name_localized")]
       public String localizedName{get;set;}
       [JsonProperty(PropertyName = "parent_category")]
       public Category category{get;set;}
    }
}
