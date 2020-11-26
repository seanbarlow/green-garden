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
            DeviceTypeSeeder.Seed(modelBuilder);
            MessageTypeSeeder.Seed(modelBuilder);
            EventTypeSeeder.Seed(modelBuilder);
            ActionTypeSeeder.Seed(modelBuilder);
        }
    }
}
