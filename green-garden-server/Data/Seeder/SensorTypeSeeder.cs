using green_garden_server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Data.Seeder
{
    public class SensorTypeSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Sensor Type - id: 2
            modelBuilder.Entity<Lookup>().HasData(
                new Lookup
                {
                    Id = 100,
                    LookupTypeId = 1,
                    UniqueId = "pump",
                    Name = "Pump",
                    Description = "Pump"
                }, new Lookup
                {
                    Id = 101,
                    LookupTypeId = 1,
                    UniqueId = "light",
                    Name = "Light",
                    Description = "Light"
                },
                new Lookup
                {
                    Id = 102,
                    LookupTypeId = 1,
                    UniqueId = "phmeter",
                    Name = "pH Meter",
                    Description = "pH Meter"
                },
                new Lookup
                {
                    Id = 103,
                    LookupTypeId = 1,
                    UniqueId = "fan",
                    Name = "Fan",
                    Description = "Fan"
                },
                new Lookup
                {
                    Id = 104,
                    LookupTypeId = 1,
                    UniqueId = "waterlevel",
                    Name = "Water Level",
                    Description = "Water Level"
                },
                new Lookup
                {
                    Id = 105,
                    LookupTypeId = 1,
                    UniqueId = "humidity",
                    Name = "Humidity",
                    Description = "Humidity"
                },
                new Lookup
                {
                    Id = 106,
                    LookupTypeId = 1,
                    UniqueId = "temperature",
                    Name = "Temperature",
                    Description = "Temperature"
                });
        }
    }
}
