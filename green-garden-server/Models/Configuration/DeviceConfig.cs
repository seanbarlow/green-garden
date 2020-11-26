using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Models.Configuration
{
    public class DeviceConfig : BaseModelConfig<Device>
    {
        public override void Configure(EntityTypeBuilder<Device> builder)
        {
            base.Configure(builder);
            builder.Property(t => t.DeviceId)
                .IsRequired();
            builder.HasMany(t => t.Sensors)
                .WithOne(t => t.Device)
                .HasForeignKey(t => t.DeviceId);
            builder.HasMany(t => t.Commands)
                .WithOne(t => t.Device)
                .HasForeignKey(t => t.DeviceId);
            builder.HasMany(t => t.Events)
                .WithOne(t => t.Device)
                .HasForeignKey(t => t.DeviceId);

        }
    }
}