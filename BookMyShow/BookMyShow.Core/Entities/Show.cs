namespace BookMyShow.Core.Entities
{
    public partial class Show
    {
        public Show()
        {
            Bookings = new HashSet<Booking>();
            ShowSeats = new HashSet<ShowSeat>();
        }

        public int ShowId { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int CinemaHallId { get; set; }
        public int Firstclass { get; set; }
        public int SecondClass { get; set; }
        public int ThirdClass { get; set; }
        public int? MovieId { get; set; }

        public virtual CinemaHall CinemaHall { get; set; } = null!;
        public virtual Movie? Movie { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<ShowSeat> ShowSeats { get; set; }
    }
}
