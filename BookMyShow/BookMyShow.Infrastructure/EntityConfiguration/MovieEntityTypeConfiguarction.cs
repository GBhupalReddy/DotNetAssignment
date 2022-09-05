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
    internal class MovieEntityTypeConfiguarction : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movie");

            builder.Property(e => e.Country)
                .HasMaxLength(64)
                .IsUnicode(false);

            builder.Property(e => e.Description)
                .HasMaxLength(512)
                .IsUnicode(false);

            builder.Property(e => e.Duration)
                .HasMaxLength(40)
                .IsUnicode(false);

            builder.Property(e => e.Genre)
                .HasMaxLength(64)
                .IsUnicode(false);

            builder.Property(e => e.Language)
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.Property(e => e.ReleaseDate).HasColumnType("date");

            builder.Property(e => e.Tittle)
                .HasMaxLength(256)
                .IsUnicode(false);
        }
    }
}
