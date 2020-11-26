using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Models.Configuration
{
    public class LookupTypeConfig : BaseModelConfig<LookupType>
    {
        public override void Configure(EntityTypeBuilder<LookupType> builder)
        {
            base.Configure(builder);
            builder.Property(t => t.Name)
                .IsRequired();
            builder.Property(t => t.UniqueId)
                .IsRequired();
            builder.HasIndex(i => i.UniqueId)
                            .IsUnique();
            builder.HasMany(t => t.Lookups)
                .WithOne(t => t.LookupType)
                .HasForeignKey(t => t.LookupTypeId);
        }
    }
}
