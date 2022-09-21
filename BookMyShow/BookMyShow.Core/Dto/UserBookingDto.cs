namespace BookMyShow.Core.Dto
{
    public class UserBookingDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int BookingId { get; set; }
        public int NumberOfSeats { get; set; }
        public DateTime Timestamp { get; set; }
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public decimal DicountCoupon { get; set; }
        public int RemoteTransactionId { get; set; }
        public string CinemaHallName { get; set; } = null!;
        public string CinemaName { get; set; } = null!;
        public string CityName { get; set; } = null!;

    }
}
