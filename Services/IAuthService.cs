using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Wayplot_Backend.DTOs;
using Wayplot_Backend.Models;

namespace Wayplot_Backend.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(string email, string password);
        // Task Register(UserRegisterDTO payload);
    }
}
