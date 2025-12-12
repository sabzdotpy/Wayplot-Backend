using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wayplot_Backend.Constants;

namespace Wayplot_Backend.Utilities
{
    public class JwtUtility : IJwtUtil
    {

        private readonly IConfiguration _configuration;

        public JwtUtility(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateJwtToken(Guid id, string email, UserRole role, List<string> scopes)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // unique ID for this token
            
                new Claim("role", role.ToString()),
                new Claim("scopes", string.Join(",", scopes))
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1), // valid for 1 hour
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
