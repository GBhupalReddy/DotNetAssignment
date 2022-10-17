using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow.Core.Dto
{
    public class movieVDto
    {
        public string? Tittle { get; set; }
        public string Description { get; set; } = null!;
        public TimeSpan? Duration { get; set; }
        public string Language { get; set; } = null!;
        public string ReleaseDate { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string ImgPath { get; set; } = null!;
    }
}
