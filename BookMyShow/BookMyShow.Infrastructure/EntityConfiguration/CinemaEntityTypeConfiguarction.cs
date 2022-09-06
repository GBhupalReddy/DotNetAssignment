using BookMyShow.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMyShow.Infrastructure.EntityConfiguration
{
    internal class CinemaEntityTypeConfiguarction : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder.ToTable("Cinema");

            builder.Property(e => e.Name)
                .HasMaxLength(64)
                .IsUnicode(false);

            builder.HasOne(d => d.City)
                .WithMany(p => p.Cinemas)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cinema__CityId__276EDEB3");
        }
    }
}
