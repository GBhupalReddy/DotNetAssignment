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
