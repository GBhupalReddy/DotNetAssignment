using System;
using System.Collections.Generic;

namespace BookMyShowProject.Core.Entities
{
    public partial class User
    {
        public User()
        {
            Bookings = new HashSet<Booking>();
        }

        public int UserId { get; set; }
        public string? Name { get; set; }
        public string Passoword { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
