using CashOverflow.Application.Common.Interfaces;
using CashOverflow.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CashOverflow.Infrastructure.Identity
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;
        public JwtService(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateToken(ApplicationUser user)
        {

            var claims = new[] {
             
              new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
             
              new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),

              new Claim(ClaimTypes.Email, user.Email),

              new Claim("OrganizationId", "DEMO"),

              new Claim("CompanyId", "DEMO")
            };

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"])),
                SecurityAlgorithms.HmacSha256
                );


            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddDays(1)
                );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
