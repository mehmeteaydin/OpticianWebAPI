using OpticianWebAPI.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OpticianWebAPI.Services.abstracts;
using OpticianWebAPI.DatabaseContext;

namespace OpticianWebAPI.Services.concretes
{
    public class AuthService(IConfiguration configuration, ILogger<AuthService> logger ,AppDbContext appDbContext) : IAuthService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly AppDbContext _appDbContext = appDbContext;
        private readonly ILogger _logger = logger;
        public string? Login(LoginRequest loginRequest)
        {
            if (loginRequest.Username != "admin" || loginRequest.Password != "12345")
            {
                return null;
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, loginRequest.Username),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}