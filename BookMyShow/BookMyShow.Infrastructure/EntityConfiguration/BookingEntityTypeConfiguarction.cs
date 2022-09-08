using BookMyShow.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace BookMyShow.Infrastructure.EntityConfiguration
{
    internal class BookingEntityTypeConfiguarction : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {

            builder.ToTable("Booking");

                builder.Property(e => e.Status).HasColumnName("status");

                builder.Property(e => e.Timestamp).HasColumnType("datetime");

                builder.HasOne(d => d.Show)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.ShowId)
                    .HasConstraintName("FK__Booking__ShowId__5535A963");

                builder.HasOne(d => d.User)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Booking__UserId__5441852A");

        }
    }
}
