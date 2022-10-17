using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Service
{
    public interface IAuthService
    {
        (string password, string passwordSalt) PasswordEncryption(string password, string? passwordSalt = null);
        string GenerateToken(User user);
    }
}