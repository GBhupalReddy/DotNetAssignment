using BookMyShow.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Dapper.SqlMapper;

namespace BookMyShow.Infrastructure.EntityConfiguration
{
    internal class SeatTypePriceEntityTypeConfiguarction : IEntityTypeConfiguration<SeatTypePrice>
    {
        public void Configure(EntityTypeBuilder<SeatTypePrice> builder)
        {
            builder.HasKey(e => e.SeatType)
                   .HasName("PK__SeatType__B9EB646BB2DED0BE");

            builder.ToTable("SeatTypePrice");

            builder.Property(e => e.SeatType).ValueGeneratedNever();

            builder.Property(e => e.Price).HasColumnType("decimal(8, 2)");
        }
    }
}
