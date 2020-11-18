using green_garden_server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Data
{
    public class GreenGardenContext : DbContext
    {
        public GreenGardenContext(DbContextOptions<GreenGardenContext> options) : base(options) { }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Log> Logs { get; set; }

    }
}
