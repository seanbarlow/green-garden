using green_garden_server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Data.Seeder
{
    public static class MessageTypeSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Message Type - id: 3
            modelBuilder.Entity<Lookup>().HasData(
                new Lookup
                {
                    Id = 14,
                    LookupTypeId = 3,
                    Name = "Reading",
                    Description = "Reading"
                }, new Lookup
                {
                    Id = 15,
                    LookupTypeId = 3,
                    Name = "Value",
                    Description = "Value"
                },
                new Lookup
                {
                    Id = 16,
                    LookupTypeId = 3,
                    Name = "Status",
                    Description = "Status"
                },
                new Lookup
                {
                    Id = 17,
                    LookupTypeId = 3,
                    Name = "Event",
                    Description = "Event"
                });
        }
    }
}
