using System.Runtime.CompilerServices;
using Wayplot_Backend.Constants;
using Wayplot_Backend.DTOs;
using Wayplot_Backend.Models;

namespace Wayplot_Backend.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<List<User>> GetAllExcludeDeleted();
        Task<User?> Get(Guid id);
        Task Update(Guid id, UserUpdateDTO payload);
        Task Delete(Guid id);
        Task ChangeUserRole(Guid id, UserRole role);
        Task ChangeUserStatus(Guid id, UserStatus status);
    }
}
