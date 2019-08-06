using System;
using Db;
using Db.Utils;

namespace DevelopData
{
    public static class Tools
    {
        public static void Truncate()
        {
            using (var db = new ApplicationDbContext())
            {
                db.JoinClientsTariffs.Clear();
                db.WhiteAddresses.Clear();
                db.GreyAddresses.Clear();
                db.Houses.Clear();
                db.Tariffs.Clear();
                db.Subnets.Clear();
                db.Clients.Clear();
                db.SystemUsers.Clear();

                db.SaveChanges();
            }
        }
    }
}