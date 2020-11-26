using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Models.Configuration
{
    public class CommandConfig : BaseModelConfig<Command>
    {
        public override void Configure(EntityTypeBuilder<Command> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Sent)
                .HasDefaultValue(false);
            builder.Property(x => x.Minutes)
                .HasDefaultValue(0);
            builder.HasOne(x => x.ActionType)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Device)
                .WithMany(x => x.Commands)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.SensorType)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
