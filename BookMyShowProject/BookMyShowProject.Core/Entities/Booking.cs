using System;
using System.Collections.Generic;

namespace BookMyShowProject.Core.Entities
{
    public partial class Booking
    {
        public Booking()
        {
            Payments = new HashSet<Payment>();
            ShowSeats = new HashSet<ShowSeat>();
        }

        public int BookingId { get; set; }
        public int NumberOfSeats { get; set; }
        public DateTime Timestamp { get; set; }
        public int Status { get; set; }
        public int? UserId { get; set; }
        public int? ShowId { get; set; }

        public virtual Show? Show { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<ShowSeat> ShowSeats { get; set; }
    }
}
