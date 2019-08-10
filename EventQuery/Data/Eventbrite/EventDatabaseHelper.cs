using EventQuery.Data.DTO;
using EventQuery.Models.Eventbrite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EventQuery.Data.Eventbrite
{
    public class EventDatabaseHelper
    {
        public static void SaveEventToDb(List<TblEvent> myEvents)
        {
             
             
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            { 
                try
                { 
                    ctx.Events.AddRange(myEvents);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                } 
            }
        }
    }
}