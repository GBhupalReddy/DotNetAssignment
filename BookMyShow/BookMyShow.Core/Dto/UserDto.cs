using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow.Core.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string Passoword { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}
