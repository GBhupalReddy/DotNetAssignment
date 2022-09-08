using BookMyShow.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMyShow.Infrastructure.EntityConfiguration
{
    internal class PaymentEntityTypeConfiguarction : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payment");

            builder.Property(e => e.Amount).HasColumnType("decimal(8, 2)");

            builder.Property(e => e.DicountCoupon).HasColumnType("decimal(7, 2)");

            builder.Property(e => e.TimeStamp).HasColumnType("datetime");

            builder.HasOne(d => d.Booking)
                .WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment__Booking__5CD6CB2B"); ;

        }
    }
}
