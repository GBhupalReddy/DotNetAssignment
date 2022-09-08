using BookMyShow.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
