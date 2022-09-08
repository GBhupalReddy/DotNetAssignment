using BookMyShow.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMyShow.Infrastructure.EntityConfiguration
{
    internal class UserEntityTypeConfiguarction : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(e => e.Phone, "UQ__Users__5C7E359E6F8A3028")
                    .IsUnique();

            builder.HasIndex(e => e.Email, "UQ__Users__A9D10534CB8CF1F2")
                .IsUnique();

            builder.Property(e => e.Email)
                .HasMaxLength(64)
                .IsUnicode(false);

            builder.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Phone)
                .HasMaxLength(16)
                .IsUnicode(false);

            builder.Property(e => e.UserName)
                .HasMaxLength(64)
                .IsUnicode(false);

        }
    }
}
