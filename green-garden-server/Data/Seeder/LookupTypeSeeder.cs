using green_garden_server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Data.Seeder
{
    public static class LookupTypeSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Lookup Types
            modelBuilder
                .Entity<LookupType>().HasData(
                new LookupType
                {
                    Id = 2,
                    Name = "Sensor Types",
                    UniqueId = "sensortypes",
                    Description = "The type of sensors we are monitoring and controlling"
                },
                new LookupType
                {
                    Id = 3,
                    Name = "Message Types",
                    UniqueId = "messagetypes",
                    Description = "Type of message we recieved."
                },
                new LookupType
                {
                    Id = 4,
                    Name = "Data Types",
                    UniqueId = "datatypes",
                    Description = "Type of data we are dealing with"
                },
                new LookupType
                {
                    Id = 5,
                    Name = "Event Types",
                    UniqueId = "eventtypes",
                    Description = "Event Type from devices"
                },
                new LookupType
                {
                    Id = 6,
                    Name = "Action Types",
                    UniqueId = "actiontypes",
                    Description = "Action Type from devices"
                });
        }
    }
}
