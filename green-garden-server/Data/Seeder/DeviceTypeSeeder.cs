using green_garden_server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Data.Seeder
{
    public class DeviceTypeSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Device Type - id: 2
            modelBuilder.Entity<Lookup>().HasData(
                new Lookup
                {
                    Id = 7,
                    LookupTypeId = 2,
                    Name = "Pump",
                    Description = "Pump"
                }, new Lookup
                {
                    Id = 8,
                    LookupTypeId = 2,
                    Name = "Light",
                    Description = "Light"
                },
                new Lookup
                {
                    Id = 9,
                    LookupTypeId = 2,
                    Name = "pH Meter",
                    Description = "pH Meter"
                },
                new Lookup
                {
                    Id = 10,
                    LookupTypeId = 2,
                    Name = "Fan",
                    Description = "Fan"
                },
                new Lookup
                {
                    Id = 11,
                    LookupTypeId = 2,
                    Name = "Water Level",
                    Description = "Water Level"
                },
                new Lookup
                {
                    Id = 12,
                    LookupTypeId = 2,
                    Name = "Humidity",
                    Description = "Humidity"
                },
                new Lookup
                {
                    Id = 13,
                    LookupTypeId = 2,
                    Name = "Temerature",
                    Description = "Temperature"
                });
        }
    }
}
