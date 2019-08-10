using System;
using Newtonsoft.Json;
namespace EventQuery.Models.Eventbrite
{
    public class TicketAvailability
    {
        [JsonProperty(PropertyName="has_available_tickets")]
        public bool hasAvailableTickets{get;set;}
            [JsonProperty(PropertyName="is_sold_out")]
        public bool isSoldOut{get;set;}
            [JsonProperty(PropertyName="minimum_ticket_price")]
        public EventPrice minEventPrice{get;set;}
          [JsonProperty(PropertyName="maximum_ticket_price")]
        public EventPrice maxEventPrice{get;set;}
  [JsonProperty(PropertyName="start_sales_date")]
        public EventDate startSalesDate{get;set;}

    }
}