namespace BookMyShow.Core.Entities
{
    public partial class CinemaHall
    {
        public CinemaHall()
        {
            CinemaSeats = new HashSet<CinemaSeat>();
            Shows = new HashSet<Show>();
        }

        public int CinemaHallId { get; set; }
        public string CinemaHallName { get; set; } = null!;
        public int TotalSeats { get; set; }
        public int CinemaId { get; set; }

        public virtual Cinema Cinema { get; set; } = null!;
        public virtual ICollection<CinemaSeat> CinemaSeats { get; set; }
        public virtual ICollection<Show> Shows { get; set; }
    }
}
