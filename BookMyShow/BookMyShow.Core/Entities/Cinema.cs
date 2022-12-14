namespace BookMyShow.Core.Entities
{
    public partial class Cinema
    {
        
            public Cinema()
            {
                CinemaHalls = new HashSet<CinemaHall>();
            }

            public int CinemaId { get; set; }
            public string CinemaName { get; set; } = null!;
            public int TotalCinemaHalls { get; set; }
            public int CityId { get; set; }

            public virtual City City { get; set; } = null!;
            public virtual ICollection<CinemaHall> CinemaHalls { get; set; }
            }
        
}
