namespace BookMyShow.Core.Entities
{
    public class BookingUser
    {
        public int NumberOfSeats { get; set; }
        public DateTime Timestamp { get; set; }
        public int Status { get; set; }
        public int? UserId { get; set; }
        public int ShowId { get; set; }
        public int SeatType { get; set; }
    }
}
