using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow.Core.Dto
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public int NumberOfSeats { get; set; }
        public DateTime Timestamp { get; set; }
        public int Status { get; set; }
        public int? UserId { get; set; }
        public int? ShowId { get; set; }
    }
}
