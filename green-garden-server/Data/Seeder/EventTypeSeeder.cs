using green_garden_server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace green_garden_server.Data.Seeder
{
    public static class EventTypeSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Event Type = 2
            modelBuilder.Entity<Lookup>().HasData(
                new Lookup
                {
                    Id = 20,
                    LookupTypeId = 2,
                    UniqueId = "update",
                    Name = "Update Event",
                    Description = "The sensor sent an update"
                }, new Lookup
                {
                    Id = 21,
                    LookupTypeId = 2,
                    UniqueId = "change",
                    Name = "Change Event",
                    Description = "A sensor setting has changed."
                });
        }
    }
}
