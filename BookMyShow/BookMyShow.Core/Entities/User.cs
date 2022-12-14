namespace BookMyShow.Core.Entities
{
    public partial class User
    {
        public User()
        {
            Bookings = new HashSet<Booking>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public bool isAdmin { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
