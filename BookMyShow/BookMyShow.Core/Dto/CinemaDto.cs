namespace BookMyShow.Core.Dto
{
    public class CinemaDto
    {
        public int CinemaId { get; set; }
        public string CinemaName { get; set; } = null!;
        public int TotalCinemaHalls { get; set; }
    }
}
