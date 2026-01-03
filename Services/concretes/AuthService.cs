using OpticianWebAPI.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OpticianWebAPI.Services.abstracts;
using OpticianWebAPI.DatabaseContext;
using System.Security.Cryptography;
using OpticianWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace OpticianWebAPI.Services.concretes
{
    public class AuthService(IConfiguration configuration,ILogger<AuthService> logger,AppDbContext appDbContext) : IAuthService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly AppDbContext _appDbContext = appDbContext;
        private readonly ILogger<AuthService> _logger = logger;
        public async Task<string?> Login(LoginRequest loginRequest)
        {
            var user = _appDbContext.Users.FirstOrDefault(x => x.Username == loginRequest.Username);

            if (user == null)
            {
                _logger.LogWarning("Kullanıcı bulunamadı: {username}", loginRequest.Username);
                return null;
            }

            if (!VerifyPasswordHash(loginRequest.Password, user.PasswordHash, user.PasswordSalt))
            {
                _logger.LogWarning("Şifre hatalı: {username}", loginRequest.Username);
                return null;
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            _logger.LogInformation("Giriş Başarılı. Username: {loginUsername}", user.Username);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> RegisterUser(RegisterUserRequest registerUserRequest)
        {
            var existingUser = await _appDbContext.Users
                .AnyAsync(u => u.Username == registerUserRequest.Username);

            if (existingUser)
            {
                return false;
            }

            CreatePasswordHash(registerUserRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Username = registerUserRequest.Username,
                Role = Enum.Parse<RoleType>(registerUserRequest.Role),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _appDbContext.Users.AddAsync(newUser);
            await _appDbContext.SaveChangesAsync();

            return true;   
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); // Şifreyi şifreler
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }
    }
}