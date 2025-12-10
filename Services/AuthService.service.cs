using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Wayplot_Backend.DTOs;
using Wayplot_Backend.Models;
using Wayplot_Backend.Repositories;

namespace Wayplot_Backend.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly IAuthRepository _repo;

        public AuthService(IAuthRepository repo)
        {
            _repo = repo;
        }

        public Task<LoginResponseDto> Login(string email, string password)
        {
            return _repo.Login(email, password);
        }

    }
}
