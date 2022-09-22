using System.ComponentModel.DataAnnotations;

namespace BookMyShow.ViewModel
{
    public  class BookingVm
    {
        [Required]
        public int NumberOfSeats { get; set; }
        public DateTime Timestamp { get; set; }
        public int Status { get; set; }
        [Required]
        public int? UserId { get; set; }
        [Required]
        public int? ShowId { get; set; }


    }
}
