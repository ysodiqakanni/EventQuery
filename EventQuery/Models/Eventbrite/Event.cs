using System.Threading;
using System.Net.Http;
using System;
using Newtonsoft.Json;

namespace EventQuery.Models.Eventbrite
{
   public class Event
    {
       public String id { get; set; }
       public EventTextHtml name {get;set;}
        public EventTextHtml description{get;set;}
        public String status{get;set;}
        [JsonProperty(PropertyName="organization_id")]
        public String organizationId{get;set;}
 
        public Venue venue{get;set;}
        public Category category{get;set;}
        
 [JsonProperty(PropertyName="ticket_availability")]
        public TicketAvailability ticketAvailability{get;set;}


    }
}