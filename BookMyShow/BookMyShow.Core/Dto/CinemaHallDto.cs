using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow.Core.Dto
{
    public class CinemaHallDto
    {
        public int CinemaHallId { get; set; }
        public string Name { get; set; } = null!;
        public int TotalSeats { get; set; }
        public int CinemaId { get; set; }
    }
}
