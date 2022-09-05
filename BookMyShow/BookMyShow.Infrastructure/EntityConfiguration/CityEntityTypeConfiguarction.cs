using BookMyShow.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow.Infrastructure.EntityConfiguration
{
    internal class CityEntityTypeConfiguarction : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("City");

                builder.HasIndex(e => e.Name, "UQ__City__737584F64A273F1A")
                    .IsUnique();

                builder.Property(e => e.Name)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                builder.Property(e => e.State)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                builder.Property(e => e.ZipCode)
                    .HasMaxLength(16)
                    .IsUnicode(false);
        }
    }
}
