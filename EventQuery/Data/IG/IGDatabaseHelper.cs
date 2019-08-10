using EventQuery.Models.IG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventQuery.Data.IG
{
    public class IGDatabaseHelper
    {
        public static void SaveUserInfoToDb(List<UserInformation> userInfos)
        {


            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                try
                {
                    ctx.UserInformations.AddRange(userInfos);
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