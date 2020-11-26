using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Models.Configuration
{
    public class LookupConfig : BaseModelConfig<Lookup>
    {
        public override void Configure(EntityTypeBuilder<Lookup> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Name)
                .IsRequired();
            builder.Property(p => p.Description)
                .IsRequired();
        }
    }
}
