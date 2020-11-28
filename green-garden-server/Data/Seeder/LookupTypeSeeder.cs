using green_garden_server.Models;
using Microsoft.EntityFrameworkCore;
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
                    Id = 1,
                    Name = "Sensor Types",
                    UniqueId = "sensortypes",
                    Description = "The type of sensors we are monitoring and controlling"
                },
                new LookupType
                {
                    Id = 2,
                    Name = "Event Types",
                    UniqueId = "eventtypes",
                    Description = "Event Type from devices"
                },
                new LookupType
                {
                    Id = 3,
                    Name = "Action Types",
                    UniqueId = "actiontypes",
                    Description = "Action Type from devices"
                });
        }
    }
}
