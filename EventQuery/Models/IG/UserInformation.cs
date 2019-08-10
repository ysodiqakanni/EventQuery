using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventQuery.Models.IG
{
    public class UserInformation
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }
        public string RetrievedUsing { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}