using BookMyShowProject.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowProject.infrastructure.EntityConfiguration
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
                .HasConstraintName("FK__Booking__ShowId__656C112C");

            builder.HasOne(d => d.User)
                .WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Booking__UserId__6477ECF3");

        }
    }
}
