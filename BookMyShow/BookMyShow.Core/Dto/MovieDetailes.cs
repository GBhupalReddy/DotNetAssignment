namespace BookMyShow.Core.Dto
{
    public class MovieDetailes
    {
        public int ShowId { get; set; }
        public string ShowTiming { get; set; } = null!;
        public string CinemaName { get; set; } = null!;
        public string Language { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string CinemaHallName { get; set; } = null!;
        public string CityName { get; set; } = null!;
        public int FirstClass { get; set; }
        public int SecondClass { get; set; }
        public int ThirdClass { get; set; }
       
    }
}
