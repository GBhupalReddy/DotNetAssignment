using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow.Infrastructure.EntityConfiguration
{
    internal class UserEntityTypeConfiguarction : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasIndex(e => e.Phone, "UQ__Users__5C7E359E3BFEFA97")
                .IsUnique();

            builder.HasIndex(e => e.Email, "UQ__Users__A9D1053490946A26")
                .IsUnique();

            builder.Property(e => e.Email)
                .HasMaxLength(64)
                .IsUnicode(false);

            builder.Property(e => e.Name)
                .HasMaxLength(64)
                .IsUnicode(false);

            builder.Property(e => e.Passoword)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Phone)
                .HasMaxLength(16)
                .IsUnicode(false);

        }
    }
}
