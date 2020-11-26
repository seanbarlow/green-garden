using green_garden_server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Data.Seeder
{
    public static class ActionTypeSeeder
    {
        public static void Seed(ModelBuilder modelBuidler)
        {
            //Action Type = 6
            modelBuidler.Entity<Lookup>().HasData(
             new Lookup
             {
                 Id = 25,
                 LookupTypeId = 6,
                 Name = "on",
                 Description = "On"
             }
             , new Lookup
             {
                 Id = 26,
                 LookupTypeId = 6,
                 Name = "off",
                 Description = "Off"
             }
             , new Lookup
             {
                 Id = 27,
                 LookupTypeId = 6,
                 Name = "lightonseconds",
                 Description = "lightonseconds"
             }
             , new Lookup
             {
                 Id = 28,
                 LookupTypeId = 6,
                 Name = "lightoffseconds",
                 Description = "lightoffseconds"
             }
             , new Lookup
             {
                 Id = 29,
                 LookupTypeId = 6,
                 Name = "pumponseconds",
                 Description = "pumponseconds"
             }
             , new Lookup
             {
                 Id = 30,
                 LookupTypeId = 6,
                 Name = "pumpoffseconds",
                 Description = "pumpoffseconds"
             });
        }
    }
}
