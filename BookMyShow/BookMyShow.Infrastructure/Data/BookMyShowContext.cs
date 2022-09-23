using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Extension;
using Microsoft.EntityFrameworkCore;

namespace BookMyShow.Infrastructure.Data
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
        public virtual DbSet<SeatTypePrice> SeatTypePrices { get; set; } = null!;   
        public virtual DbSet<Show> Shows { get; set; } = null!;
        public virtual DbSet<ShowSeat> ShowSeats { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RegisterEntityConfigurations();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
