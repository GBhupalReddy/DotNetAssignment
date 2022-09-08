using BookMyShow.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMyShow.Infrastructure.EntityConfiguration
{
    internal class ShowSeatEntityTypeConfiguarction : IEntityTypeConfiguration<ShowSeat>
    {
        public void Configure(EntityTypeBuilder<ShowSeat> builder)
        {
            builder.ToTable("ShowSeat");

            builder.Property(e => e.Price).HasColumnType("decimal(8, 2)");

            builder.HasOne(d => d.Booking)
                .WithMany(p => p.ShowSeats)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShowSeat__Bookin__59FA5E80");

            builder.HasOne(d => d.CinemaSeat)
                .WithMany(p => p.ShowSeats)
                .HasForeignKey(d => d.CinemaSeatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShowSeat__Cinema__5812160E");

            builder.HasOne(d => d.Show)
                .WithMany(p => p.ShowSeats)
                .HasForeignKey(d => d.ShowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShowSeat__ShowId__59063A47");
        }
    }
}
