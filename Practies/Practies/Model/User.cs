using System;
using System.Collections.Generic;

namespace Practies.Model
{
    public partial class User
    {
        public User()
        {
            Bookings = new HashSet<Booking>();
        }

        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
