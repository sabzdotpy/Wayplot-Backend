using System.Runtime.CompilerServices;
using Wayplot_Backend.DTOs;
using Wayplot_Backend.Models;
using Wayplot_Backend.Repositories;

namespace Wayplot_Backend.Services
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public Task<List<User>> GetAll() => _repo.GetAll();
        public Task<User?> Get(Guid id) => _repo.Get(id);
        public Task Update(Guid id, UserUpdateDTO payload)
        {
            Console.WriteLine("in Service now. Sending to repo.");
            return _repo.Update(id, payload);
        }
    }
}
