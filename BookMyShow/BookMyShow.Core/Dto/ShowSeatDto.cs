using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow.Core.Dto
{
    public class ShowSeatDto
    {
        public int ShowSeatId { get; set; }
        public int Status { get; set; }
        public decimal Price { get; set; }
        public int CinemaSeatId { get; set; }
        public int ShowId { get; set; }
        public int BookingId { get; set; }
    }
}
