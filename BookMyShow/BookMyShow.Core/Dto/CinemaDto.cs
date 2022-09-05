using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow.Core.Dto
{
    public class CinemaDto
    {
        public int CinemaSeatId { get; set; }
        public int? SeatNumber { get; set; }
        public int Type { get; set; }
        public int CinemaHallId { get; set; }
    }
}
