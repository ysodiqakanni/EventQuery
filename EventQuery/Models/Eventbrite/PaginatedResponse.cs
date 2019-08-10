using System.Threading;
using System.Net.Http;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

namespace EventQuery.Models.Eventbrite
{
   public abstract class PaginatedResponse
    {
        [JsonProperty(PropertyName="pagination")]
       public Pagination Pagination{get;set;}
    }
}
