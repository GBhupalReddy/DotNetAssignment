namespace BookMyShow.Core.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}
