using System;
using System.Collections.Generic;

namespace BookMyShowProject.Core.Entities
{
    public partial class Show
    {
        public Show()
        {
            Bookings = new HashSet<Booking>();
            ShowSeats = new HashSet<ShowSeat>();
        }

        public int ShowId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int CinemaHallId { get; set; }
        public int? MovieId { get; set; }

        public virtual CinemaHall CinemaHall { get; set; } = null!;
        public virtual Movie? Movie { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<ShowSeat> ShowSeats { get; set; }
    }
}
