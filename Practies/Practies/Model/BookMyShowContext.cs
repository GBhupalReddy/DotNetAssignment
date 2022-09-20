using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Practies.Model
{
    public partial class BookMyShowContext : DbContext
    {
        public BookMyShowContext()
        {
        }

        public BookMyShowContext(DbContextOptions<BookMyShowContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Cinema> Cinemas { get; set; } = null!;
        public virtual DbSet<CinemaHall> CinemaHalls { get; set; } = null!;
        public virtual DbSet<CinemaSeat> CinemaSeats { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Show> Shows { get; set; } = null!;
        public virtual DbSet<ShowSeat> ShowSeats { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= (localDb)\\MSSQLLocalDB; DataBase=BookMyShow;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.HasOne(d => d.Show)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.ShowId)
                    .HasConstraintName("FK__Booking__ShowId__1DB06A4F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Booking__UserId__1CBC4616");
            });

            modelBuilder.Entity<Cinema>(entity =>
            {
                entity.ToTable("Cinema");

                entity.Property(e => e.CinemaName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Cinemas)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cinema__CityId__276EDEB3");
            });

            modelBuilder.Entity<CinemaHall>(entity =>
            {
                entity.ToTable("CinemaHall");

                entity.Property(e => e.CinemaHallName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.Cinema)
                    .WithMany(p => p.CinemaHalls)
                    .HasForeignKey(d => d.CinemaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CinemaHal__Cinem__44FF419A");
            });

            modelBuilder.Entity<CinemaSeat>(entity =>
            {
                entity.ToTable("CinemaSeat");

                entity.HasOne(d => d.CinemaHall)
                    .WithMany(p => p.CinemaSeats)
                    .HasForeignKey(d => d.CinemaHallId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CinemaSea__Cinem__73BA3083");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.HasIndex(e => e.CityName, "UQ__City__886159E559CB46F0")
                    .IsUnique();

                entity.Property(e => e.CityName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movie");

                entity.Property(e => e.Country)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.Genre)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Language)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ReleaseDate).HasColumnType("date");

                entity.Property(e => e.Tittle)
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.Amount).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.DicountCoupon).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Payment__Booking__25518C17");
            });

            modelBuilder.Entity<Show>(entity =>
            {
                entity.ToTable("Show");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.CinemaHall)
                    .WithMany(p => p.Shows)
                    .HasForeignKey(d => d.CinemaHallId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Show__CinemaHall__5070F446");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Shows)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__Show__MovieId__5165187F");
            });

            modelBuilder.Entity<ShowSeat>(entity =>
            {
                entity.ToTable("ShowSeat");

                entity.Property(e => e.Price).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.ShowSeats)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ShowSeat__Bookin__22751F6C");

                entity.HasOne(d => d.CinemaSeat)
                    .WithMany(p => p.ShowSeats)
                    .HasForeignKey(d => d.CinemaSeatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ShowSeat__Cinema__208CD6FA");

                entity.HasOne(d => d.Show)
                    .WithMany(p => p.ShowSeats)
                    .HasForeignKey(d => d.ShowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ShowSeat__ShowId__2180FB33");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Phone, "UQ__Users__5C7E359E6F8A3028")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Users__A9D10534CB8CF1F2")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
