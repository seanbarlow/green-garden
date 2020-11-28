using green_garden_server.Models;
using green_garden_server.Models.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Data
{
    public class GreenGardenContext : DbContext
    {
        public GreenGardenContext(DbContextOptions<GreenGardenContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CommandConfig());
            modelBuilder.ApplyConfiguration(new DeviceConfig());
            modelBuilder.ApplyConfiguration(new DeviceEventConfig());
            modelBuilder.ApplyConfiguration(new LookupConfig());
            modelBuilder.ApplyConfiguration(new LookupTypeConfig());
            modelBuilder.ApplyConfiguration(new SensorConfig());

            modelBuilder.Seed();
        }

        public DbSet<Command> Commands { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceEvent> Events { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<LookupType> LookupTypes { get; set; }
        public DbSet<Sensor> Sensors { get; set; }

    }

    public class GreenGardenContextFactory : IDesignTimeDbContextFactory<GreenGardenContext>
    {
        public GreenGardenContext CreateDbContext(string[] args)
        {
            var environmentName =
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<GreenGardenContext>();
            var connectionString = configuration.GetConnectionString("GreenGardenConnectionString");
            optionsBuilder.UseSqlServer(connectionString);

            return new GreenGardenContext(optionsBuilder.Options);
        }
    }
}
