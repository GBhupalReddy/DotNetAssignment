using System.ComponentModel.DataAnnotations;

namespace BookMyShow.ViewModel
{

    public class BookingUserVm
    {
        [Required]
        public int NumberOfSeats { get; set; }
        public DateTime Timestamp { get; set; }

        public int Status { get; set; }

        [Required]
        public int? UserId { get; set; }

        [Required]
        public int? ShowId { get; set; }

        [Range(1, 3)]
        [Required]
        public int? SeatType { get; set; }
    }
}
