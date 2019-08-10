using System.Threading;
using System.Net.Http;
using System;
using Newtonsoft.Json;

namespace EventQuery.Models.Eventbrite
{
   public class Category
    {
       public int id { get; set; }
       public String name {get;set;}
    [JsonProperty(PropertyName = "name_localized")]
       public String localizedName{get;set;}
    }
}
