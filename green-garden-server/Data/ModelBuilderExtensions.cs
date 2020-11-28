using green_garden_server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using green_garden_server.Data.Seeder;

namespace green_garden_server.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            LookupTypeSeeder.Seed(modelBuilder);
            SensorTypeSeeder.Seed(modelBuilder);
            EventTypeSeeder.Seed(modelBuilder);
            ActionTypeSeeder.Seed(modelBuilder);

            modelBuilder.Entity<Device>().HasData(
                new Device
                {
                    Id = 1,
                    DeviceId = "green-garden-controller"
                });

            modelBuilder.Entity<Sensor>().HasData(
                new Sensor
                {
                    Id = 1,
                    DeviceId = 1,
                    SensorTypeId = 100
                },
                new Sensor
                {
                    Id = 2,
                    DeviceId = 1,
                    SensorTypeId = 101
                });
        }
    }
}
