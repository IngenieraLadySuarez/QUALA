using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Quala.Sucursales.Api.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Quala.Sucursales.Api.Auth
{
    public class JwtService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtService(IConfiguration configuration)
        {
            _jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

            if (_jwtSettings == null ||
                string.IsNullOrWhiteSpace(_jwtSettings.Secret) ||
                string.IsNullOrWhiteSpace(_jwtSettings.Issuer) ||
                string.IsNullOrWhiteSpace(_jwtSettings.Audience))
            {
                throw new ArgumentException("JwtSettings no está configurado correctamente en appsettings.json");
            }
        }

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("role", user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
