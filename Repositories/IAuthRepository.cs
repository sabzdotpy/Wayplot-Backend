using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Wayplot_Backend.DTOs;

namespace Wayplot_Backend.Repositories
{
    public interface IAuthRepository
    {
        Task<LoginResponseDto> Login(string email, string password);
    }
}
