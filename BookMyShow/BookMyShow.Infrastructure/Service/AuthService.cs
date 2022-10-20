using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Extension;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text;

namespace BookMyShow.Infrastructure.Service
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public (string password, string passwordSalt) PasswordEncryption(string password, string? passwordSalt = null)
        {
            if (passwordSalt == null)
                passwordSalt = GenerateSalt();
            password += passwordSalt;
            password = GenerateHashPassword(password);
            return (password, passwordSalt);

        }

        private string GenerateHashPassword(string password)
        {
            string machineKey = _configuration["MachineKey"].ToString();
            var hmac = new HMACSHA1()
            {
                Key = machineKey.HexToByte()
            };
            return Convert.ToBase64String(hmac.ComputeHash(password.GetByteArray()));
        }
        private static string GenerateSalt()
        {
            int saltLength = 8;
            byte[] salt = new byte[saltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }
            return Convert.ToBase64String(salt);

        }
        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim("UserName", user.UserName),
                new Claim("Email", user.Email),
                new Claim(ClaimTypes.Role,user.isAdmin ? "admin" :"user")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
