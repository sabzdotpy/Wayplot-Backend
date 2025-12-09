using System.Runtime.CompilerServices;
using Wayplot_Backend.DTOs;
using Wayplot_Backend.Models;

namespace Wayplot_Backend.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User?> Get(Guid id);

        Task Update(Guid id, UserUpdateDTO payload);
    }
}
