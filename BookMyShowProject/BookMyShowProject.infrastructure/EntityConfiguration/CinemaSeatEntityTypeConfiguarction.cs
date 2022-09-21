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
    internal class CinemaSeatEntityTypeConfiguarction : IEntityTypeConfiguration<CinemaSeat>
    {
        public void Configure(EntityTypeBuilder<CinemaSeat> builder)
        {
            builder.ToTable("CinemaSeat");

            builder.HasOne(d => d.CinemaHall)
                .WithMany(p => p.CinemaSeats)
                .HasForeignKey(d => d.CinemaHallId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CinemaSea__Cinem__5812160E");

        }
    }
}
