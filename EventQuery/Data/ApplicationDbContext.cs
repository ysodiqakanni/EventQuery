using EventQuery.Data.DTO;
using EventQuery.Models.IG;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EventQuery.Data
{ 
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<TblEvent> Events { get; set; }
        public DbSet<UserInformation> UserInformations { get; set; }
    }
    public class DbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);
        }
    }
}