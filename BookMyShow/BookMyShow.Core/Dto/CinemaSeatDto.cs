namespace BookMyShow.Core.Dto
{
    public class CinemaSeatDto
    {
        public int CinemaSeatId { get; set; }
        public int? SeatNumber { get; set; }
        public int Type { get; set; }
        public int CinemaHallId { get; set; }
    }
}
