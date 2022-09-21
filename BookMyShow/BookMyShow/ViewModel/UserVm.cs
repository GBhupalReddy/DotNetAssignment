using System.ComponentModel.DataAnnotations;

namespace BookMyShow.ViewModel
{
    public class UserVm
    {
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? UserName { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Email { get; set; } = null!;

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Password { get; set; } = null!;

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Phone { get; set; } = null!;
    }
}
