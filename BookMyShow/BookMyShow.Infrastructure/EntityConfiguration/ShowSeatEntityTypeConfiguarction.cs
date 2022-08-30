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
                .HasConstraintName("FK__ShowSeat__Bookin__3F466844");

            builder.HasOne(d => d.CinemaSeat)
                .WithMany(p => p.ShowSeats)
                .HasForeignKey(d => d.CinemaSeatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShowSeat__Cinema__3D5E1FD2");

            builder.HasOne(d => d.Show)
                .WithMany(p => p.ShowSeats)
                .HasForeignKey(d => d.ShowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShowSeat__ShowId__3E52440B");
        }
    }
}
