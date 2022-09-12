namespace BookMyShow.ViewModel
{
    public class UserVm
    {
        public string? UserName { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        
        public string Phone { get; set; } = null!;
    }
}
