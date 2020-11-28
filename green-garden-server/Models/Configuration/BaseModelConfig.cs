using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Models.Configuration
{
    public abstract class BaseModelConfig<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseModel
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(t => t.Created)
                    .IsRequired()
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GetDate()");
            builder.Property(t => t.Updated)
                    .IsRequired()
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GetDate()");
            builder.Property(t => t.Deleted)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
