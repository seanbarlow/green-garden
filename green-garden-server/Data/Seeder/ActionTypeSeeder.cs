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
            //Action Type = 3
            modelBuidler.Entity<Lookup>().HasData(
             new Lookup
             {
                 Id = 300,
                 LookupTypeId = 3,
                 UniqueId = "on",
                 Name = "On",
                 Description = "On"
             }
             , new Lookup
             {
                 Id = 301,
                 LookupTypeId = 3,
                 UniqueId = "off",
                 Name = "Off",
                 Description = "Off"
             }
             , new Lookup
             {
                 Id = 302,
                 LookupTypeId = 3,
                 UniqueId = "lightonseconds",
                 Name = "Light on Seconds",
                 Description = "Seconds the light is on for"
             }
             , new Lookup
             {
                 Id = 303,
                 LookupTypeId = 3,
                 UniqueId = "lightoffseconds",
                 Name = "Light of Seconds",
                 Description = "Seconds the light is off"
             }
             , new Lookup
             {
                 Id = 304,
                 LookupTypeId = 3,
                 UniqueId = "pumponseconds",
                 Name = "Pump on Seconds",
                 Description = "Seconds the Pump is on"
             }
             , new Lookup
             {
                 Id = 305,
                 LookupTypeId = 3,
                 UniqueId = "pumpoffseconds",
                 Name = "Pump off Seconds",
                 Description = "Seconds the pump is off"
             });
        }
    }
}
