using EventQuery.Data;
using EventQuery.Data.DTO;
using EventQuery.Data.Eventbrite;
using EventQuery.Models.Eventbrite;
using EventQuery.Services.Evebtbrite;
using EventQuery.Services.IG;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EventQuery.Controllers
{ 
    public class HomeController : Controller
    {
        ApplicationDbContext ctx = null;
        public HomeController()
        {
            ctx = new ApplicationDbContext();
        }
        public async Task<ActionResult> Index()
        {
          

            return View();
        }
        public async Task<ActionResult> LoadFromIGUsernames(string username)
        {
            if (String.IsNullOrEmpty(username)) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var mapPath = HttpContext.Server.MapPath("~/images");
            await AppImageDownloader.BuildWithUsername(mapPath, username);
            return RedirectToAction("Success");
        }
        public async Task<ActionResult> LoadFromIGTags()
        {
            string tags = ConfigurationManager.AppSettings["IgTag"];
            if (String.IsNullOrEmpty(tags)) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var mapPath = HttpContext.Server.MapPath("~/images");
            await AppImageDownloader.BuildWithTags(mapPath, tags);
            return RedirectToAction("Success");
        }

        public ActionResult LoadFromEventBrite(string keyword, string location)
        {
            List<Event> filteredEvents = EventHelper.getAllEvents(keyword, location);
            var eventsToSaveToDb = GetUsefulEventData(filteredEvents);
            EventDatabaseHelper.SaveEventToDb(eventsToSaveToDb);
            return RedirectToAction("Success");
        }
        public ActionResult Success()
        {
            return View();
        }
        public ActionResult AllEvents()
        {
            var allEvents = ctx.Events.Take(50).ToList();
            return View(allEvents);
        }
        public ActionResult InstagramImages()
        {
            var allIgImages = ctx.UserInformations.OrderByDescending(i => i.CreatedOn).Take(50).ToList();
            return View(allIgImages);
        }
        public ActionResult ViewIgImage(string imageUri)
        {
            ViewBag.ImgUrl = imageUri;
            return View();
        }


        private List<TblEvent> GetUsefulEventData(List<Event> events)
        {
            var result = new List<TblEvent>();
            foreach (var item in events)
            {
                var evt = new TblEvent
                {
                    IdFromEventbrite = item.id,
                    Name = item.name.text,
                    Description = item.description.text,
                    OrganizationId = item.organizationId,
                    Status = item.status,
                    Venue = item.venue.name + " " + item.venue.address,
                    Category = item.category?.name,
                    TicketAvailable = !item.ticketAvailability.isSoldOut
                };
                result.Add(evt);
            }
            return result;
        }

      
        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}