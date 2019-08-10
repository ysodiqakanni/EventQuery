using System.Threading;
using System.Net.Http;
using System;
using Newtonsoft.Json;

namespace EventQuery.Models.Eventbrite
{
   public class EventTextHtml
    {
       public String text { get; set; }
       public String html {get;set;}
    }
}