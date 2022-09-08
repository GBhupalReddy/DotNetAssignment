using BookMyShow.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMyShow.Infrastructure.EntityConfiguration
{
    internal class ShowEntityTypeConfiguarction : IEntityTypeConfiguration<Show>
    {
        public void Configure(EntityTypeBuilder<Show> builder)
        {
            builder.ToTable("Show");

            builder.Property(e => e.Date).HasColumnType("date");

            builder.HasOne(d => d.CinemaHall)
                .WithMany(p => p.Shows)
                .HasForeignKey(d => d.CinemaHallId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Show__CinemaHall__5070F446");

            builder.HasOne(d => d.Movie)
                .WithMany(p => p.Shows)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK__Show__MovieId__5165187F");

        }
    }
}
