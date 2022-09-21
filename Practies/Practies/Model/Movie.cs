using System;
using System.Collections.Generic;

namespace Practies.Model
{
    public partial class Movie
    {
        public Movie()
        {
            Shows = new HashSet<Show>();
        }

        public int MovieId { get; set; }
        public string? Tittle { get; set; }
        public string Description { get; set; } = null!;
        public TimeSpan? Duration { get; set; }
        public string Language { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public string Country { get; set; } = null!;
        public string? Genre { get; set; }

        public virtual ICollection<Show> Shows { get; set; }
    }
}
