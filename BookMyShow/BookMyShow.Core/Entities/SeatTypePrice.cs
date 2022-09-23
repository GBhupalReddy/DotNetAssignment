namespace BookMyShow.Core.Entities
{
    public class SeatTypePrice
    {
        public SeatTypePrice()
        {
            CinemaSeats = new HashSet<CinemaSeat>();
        }
        public int SeatType { get; set; }
        public decimal Price { get; set; } 

        public virtual ICollection<CinemaSeat> CinemaSeats { get; set; }

    }
}
