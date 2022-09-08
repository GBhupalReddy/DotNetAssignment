using BookMyShow.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMyShow.Infrastructure.EntityConfiguration
{
    internal class CinemaHallEntityTypeConfiguarction : IEntityTypeConfiguration<CinemaHall>
    {
        public void Configure(EntityTypeBuilder<CinemaHall> builder)
        {
           

            builder.ToTable("CinemaHall");

            builder.Property(e => e.CinemaHallName)
                .HasMaxLength(64)
                .IsUnicode(false);

            builder.HasOne(d => d.Cinema)
                .WithMany(p => p.CinemaHalls)
                .HasForeignKey(d => d.CinemaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CinemaHal__Cinem__44FF419A");

        }
    }
}
