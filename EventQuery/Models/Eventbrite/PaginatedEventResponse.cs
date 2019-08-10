using System.Collections.Generic;
namespace EventQuery.Models.Eventbrite
{
    public class PaginatedEventResponse: PaginatedResponse
    {
        public List<Event> events{get;set;}
    }
}