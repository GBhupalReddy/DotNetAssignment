namespace BookMyShow.Core.Entities
{
    public partial class CinemaSeat
    {
        public CinemaSeat()
        {
            ShowSeats = new HashSet<ShowSeat>();
        }

        public int CinemaSeatId { get; set; }
        public int? SeatNumber { get; set; }
        public int Type { get; set; }
        public int CinemaHallId { get; set; }

        public virtual CinemaHall CinemaHall { get; set; } = null!;
        public virtual ICollection<ShowSeat> ShowSeats { get; set; }
    }
}
