using BookMyShow.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMyShow.Infrastructure.EntityConfiguration
{
    internal class CinemaSeatEntityTypeConfiguarction : IEntityTypeConfiguration<CinemaSeat>
    {
        public void Configure(EntityTypeBuilder<CinemaSeat> builder)
        {
            builder.ToTable("CinemaSeat");

            builder.HasOne(d => d.CinemaHall)
                .WithMany(p => p.CinemaSeats)
                .HasForeignKey(d => d.CinemaHallId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CinemaSea__Cinem__47DBAE45");

        }
    }
}
