using System;
using System.Collections.Generic;

namespace BookMyShowProject.Core.Entities
{
    public partial class City
    {
        public City()
        {
            Cinemas = new HashSet<Cinema>();
        }

        public int CityId { get; set; }
        public string Name { get; set; } = null!;
        public string State { get; set; } = null!;
        public string ZipCode { get; set; } = null!;

        public virtual ICollection<Cinema> Cinemas { get; set; }
    }
}
