namespace BookMyShow.Core.Entities
{
    public partial class Movie
    {
        public Movie()
        {
            Shows = new HashSet<Show>();
        }

        public int MovieId { get; set; }
        public string Tittle { get; set; } = null!;
        public string Description { get; set; } = null!;
        public TimeSpan? Duration { get; set; }
        public string Language { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public string Country { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string ImgPath { get; set; } = null!;

        public virtual ICollection<Show> Shows { get; set; }
    }
}
