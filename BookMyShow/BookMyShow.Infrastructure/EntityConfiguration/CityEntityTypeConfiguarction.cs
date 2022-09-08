using BookMyShow.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMyShow.Infrastructure.EntityConfiguration
{
    internal class CityEntityTypeConfiguarction : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("City");

            builder.HasIndex(e => e.CityName, "UQ__City__886159E559CB46F0")
                .IsUnique();

            builder.Property(e => e.CityName)
                .HasMaxLength(64)
                .IsUnicode(false);

            builder.Property(e => e.State)
                .HasMaxLength(64)
                .IsUnicode(false);

            builder.Property(e => e.ZipCode)
                .HasMaxLength(16)
                .IsUnicode(false);
        }
    }
}
