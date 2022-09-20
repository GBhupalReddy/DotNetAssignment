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
    internal class ShowEntityTypeConfiguarction : IEntityTypeConfiguration<Show>
    {
        public void Configure(EntityTypeBuilder<Show> builder)
        {
            builder.ToTable("Show");

            builder.Property(e => e.Date).HasColumnType("date");

            builder.Property(e => e.EndTime).HasColumnType("datetime");

            builder.Property(e => e.StartTime).HasColumnType("datetime");

            builder.HasOne(d => d.CinemaHall)
                .WithMany(p => p.Shows)
                .HasForeignKey(d => d.CinemaHallId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Show__CinemaHall__60A75C0F");

            builder.HasOne(d => d.Movie)
                .WithMany(p => p.Shows)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK__Show__MovieId__619B8048");

        }
    }
}
