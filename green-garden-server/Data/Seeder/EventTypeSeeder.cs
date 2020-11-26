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
            // Event Type = 5
            modelBuilder.Entity<Lookup>().HasData(
                new Lookup
                {
                    Id = 23,
                    LookupTypeId = 5,
                    Name = "update",
                    Description = "The sensor is updating us with its status"
                }, new Lookup
                {
                    Id = 24,
                    LookupTypeId = 5,
                    Name = "change",
                    Description = "The sensor has changed its status"
                });
        }
    }
}
