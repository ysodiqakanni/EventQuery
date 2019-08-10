using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventQuery.Data.DTO
{
    public class TblEvent
    {
        public int ID { get; set; }
        public string IdFromEventbrite { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string End { get; set; }
        public string Status { get; set; }
        public string OrgId { get; set; }
        public string OrganizationId { get; set; }
        public bool TicketAvailable { get; set; }
        public string Venue { get; set; }
        public string Category { get; set; }
    }
}