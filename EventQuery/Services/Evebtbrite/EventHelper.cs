using EventQuery.Models.Eventbrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace EventQuery.Services.Evebtbrite
{
    public static class EventHelper
    {
        public static List<Event> getAllEvents(String keyword, string location)
        {
            Console.Clear();

            Console.WriteLine($"Searching by keyword {keyword}");

            List<Event> events = new List<Event>();
            var firstPaginatedEventResponse = getEventByPage(1, keyword, location);
            events.AddRange(firstPaginatedEventResponse.events);
            long pageCount = firstPaginatedEventResponse.Pagination.pageCount;

            List<Task<PaginatedEventResponse>> paginatedEventResponsesTask = new List<Task<PaginatedEventResponse>>();

            for (int i = 2; i <= 150; i++)
            {
                paginatedEventResponsesTask.Add(Task.Run(() => getEventByPage(i, keyword, location)));
            }
            Task.WaitAll(paginatedEventResponsesTask.ToArray());
            foreach (var item in paginatedEventResponsesTask)
            {
                if (item.Exception == null && item.Result != null)
                    events.AddRange(item.Result.events);
            } 
             return events;
        }

        public static PaginatedEventResponse getEventByPage(int pageNo, string keyword, string location)
        {
            try
            {
                HttpClient _httpClient = new HttpClient();
                var paginatedTaskResponse = _httpClient.GetAsync($"https://www.eventbriteapi.com/v3/events/search/?q={keyword}&location.address={location}&token=67UFYQSD73W65VVX5OVC&page={pageNo}&expand=ticket_availability,category,venue,subcategory");
                // var paginatedTaskResponse = _httpClient.GetAsync(_baseUrl + eventSearch + $"?token={_apiKey}page={pageNo}&expand=ticket_availability,category,venue");
                Task.WaitAll(paginatedTaskResponse);

                var response = paginatedTaskResponse.GetAwaiter().GetResult();
                if (response.StatusCode != HttpStatusCode.OK)
                {

                    return null;

                }
                var paginatedEventResponse = (PaginatedEventResponse)response.Content.ReadAsAsync(typeof(PaginatedEventResponse)).Result;
                return paginatedEventResponse;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.StackTrace);
                return null;
            }
        }
        public static Boolean checkName(Event eventCheck, String keyword)
        {
            return eventCheck != null && eventCheck.name.text.Contains(keyword);
        }
        public static Boolean checkDescription(Event eventCheck, String keyword)
        {
            return eventCheck != null && eventCheck.description != null && eventCheck.description.text != null && eventCheck.description.text.Contains(keyword);
        }
        public static Boolean checkStatus(Event eventCheck, String keyword)
        {
            return eventCheck != null && eventCheck.status.Contains(keyword);
        }
        public static Boolean checkCategory(Event eventCheck, String keyword)
        {
            return eventCheck != null && eventCheck.category != null && eventCheck.category.name != null && eventCheck.category.name.Contains(keyword);
        }

        public static Boolean checkVenueName(Event eventCheck, String keyword)
        {
            return eventCheck != null && eventCheck.venue != null && eventCheck.venue.name != null && eventCheck.venue.name.Contains(keyword);
        }
        public static Boolean checkVenueAddress(Event eventCheck, String keyword)
        {
            return (eventCheck != null && eventCheck.venue != null &&
            eventCheck.venue.address != null) && ((eventCheck.venue.address.address1 != null && eventCheck.venue.address.address1.Contains(keyword))
         || (eventCheck.venue.address.address2 != null && eventCheck.venue.address.address2.Contains(keyword)) ||
         (eventCheck.venue.address.city != null && eventCheck.venue.address.city.Contains(keyword)) ||
         (eventCheck.venue.address.region != null && eventCheck.venue.address.region.Contains(keyword)) ||
         (eventCheck.venue.address.country != null && eventCheck.venue.address.country.Contains(keyword)) ||
         (eventCheck.venue.address.postalCode != null && eventCheck.venue.address.postalCode.Contains(keyword)));
        }
    }
}