using Microsoft.EntityFrameworkCore;
using Wayplot_Backend.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wayplot_Backend.Utilities;

namespace Wayplot_Backend.Repositories
{
    public sealed class AuthRepository : IAuthRepository
    {
        private readonly Database.WayplotDbContext _db;
        private readonly IJwtUtil _jwtUtil;

        public AuthRepository(Database.WayplotDbContext db, IJwtUtil jwtUtil)
        {
            _db = db;
            _jwtUtil = jwtUtil;
        }

        public async Task<LoginResponseDto> Login(string email, string password)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                return new LoginResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "Invalid email or password",
                    Token = null
                };
            }

            // jwt generation.
            string token = _jwtUtil.GenerateJwtToken(user.Id, user.Email, user.Role, user.Scopes);

            return new LoginResponseDto
            {
                IsSuccess = true,
                ErrorMessage = null,
                Token = token
            };
        }
    }
}