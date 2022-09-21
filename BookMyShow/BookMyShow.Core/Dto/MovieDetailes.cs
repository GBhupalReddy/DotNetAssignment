using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow.Core.Dto
{
    public class MovieDetailes
    {
        public string MovieName { get; set; } = null!;
        public TimeSpan? ShowTiming { get; set; }
        public string CinemaName { get; set; } = null!;
        public string Language { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string CinemaHallName { get; set; } = null!;
        public string CityName { get; set; } = null!;
    }
}
