namespace BookMyShow.Core.Dto
{
    public class ShowDto
    {
        public int ShowId { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int CinemaHallId { get; set; }
        public int? MovieId { get; set; }
    }
}
