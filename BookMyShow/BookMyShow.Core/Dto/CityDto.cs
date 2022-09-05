using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow.Core.Dto
{
    public class CityDto
    {

        public int CityId { get; set; }
        public string Name { get; set; } = null!;
        public string State { get; set; } = null!;
        public string ZipCode { get; set; } = null!;


    }
}
