namespace BookMyShow.ViewModel
{
    public class BookingUserVm
    {
        public int NumberOfSeats { get; set; }
        public DateTime Timestamp { get; set; }
        public int Status { get; set; }
        public int? UserId { get; set; }
        public int? ShowId { get; set; }
        public int? SeatType { get; set; }
    }
}
