namespace BookMyShow.Core.Dto
{
    public class CinemaDto
    {
        public int CinemaId { get; set; }
        public string Name { get; set; } = null!;
        public int TotalCinemaHalls { get; set; }
        public int CityId { get; set; }
    }
}
