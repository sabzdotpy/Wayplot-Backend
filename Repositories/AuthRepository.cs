using Microsoft.EntityFrameworkCore;
using Wayplot_Backend.DTOs;
using Wayplot_Backend.Utilities;
using Wayplot_Backend.Models;
using Wayplot_Backend.Constants;

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
            Console.Write("User status is: ");
            Console.WriteLine(user.Status);

            if (user.Status != UserStatus.ACTIVE)
            {
                return new LoginResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = $"This account is not in a valid state. Please contact your administrator.",
                    Token = null
                };
            }

            // jwt generation.
            string token = _jwtUtil.GenerateJwtToken(user.Id, user.Email, user.Role, user.Scopes);

            return new LoginResponseDto
            {
                IsSuccess = true,
                ErrorMessage = null,
                Token = token,
                Name = user.Name,
                Role = user.Role.ToString(),
            };
        }

        public async Task<LoginResponseDto> Signup(SignupRequestDto signupRequest)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == signupRequest.Email);
            if (user != null)
            {
                return new LoginResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "Invalid email or password",
                    Token = null
                };
            }

            User newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = signupRequest.Email,
                Password = signupRequest.Password,
                AuthType = signupRequest.AuthType,
                Name = signupRequest.Name,
                CreatedAt = DateTime.UtcNow,
                Role = signupRequest.UserRole,
                Scopes = signupRequest.Scopes,
                Status = UserStatus.ACTIVE,
                UpdatedAt = DateTime.UtcNow,
            };

            _db.Users.Add(newUser);
            _db.SaveChanges();

            // jwt generation.
            string token = _jwtUtil.GenerateJwtToken(newUser.Id, newUser.Email, newUser.Role, newUser.Scopes);

            return new LoginResponseDto
            {
                IsSuccess = true,
                ErrorMessage = null,
                Token = token,
                Name = newUser.Name,
                Role = newUser.Role.ToString()
            };
        }
    }
}