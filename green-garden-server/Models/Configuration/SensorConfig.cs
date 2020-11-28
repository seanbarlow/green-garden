using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Models.Configuration
{
    public class SensorConfig : BaseModelConfig<Sensor>
    {
        public override void Configure(EntityTypeBuilder<Sensor> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.LastUpdate)
                    .IsRequired()
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GetDate()");
        }
    }
}
