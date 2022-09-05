namespace BookMyShow.ViewModel
{
    public  class MovieVm
    {
   
        public string? Tittle { get; set; }
        public string Description { get; set; } = null!;
        public string? Duration { get; set; }
        public string Language { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public string Country { get; set; } = null!;
        public string? Genre { get; set; }

       
    }
}
