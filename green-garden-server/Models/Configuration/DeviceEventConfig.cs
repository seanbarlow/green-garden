using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Models.Configuration
{
    public class DeviceEventConfig : BaseModelConfig<DeviceEvent>
    {
        public override void Configure(EntityTypeBuilder<DeviceEvent> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Data)
                .IsRequired();
            builder.HasOne(x => x.Device)
                .WithMany(x => x.Events)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
