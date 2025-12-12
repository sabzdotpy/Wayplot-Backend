using Wayplot_Backend.Constants;
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
        public Task<List<User>> GetAllExcludeDeleted() => _repo.GetAllExcludeDeleted();
        public Task<User?> Get(Guid id) => _repo.Get(id);
        public Task Update(Guid id, UserUpdateDTO payload)
        {
            Console.WriteLine("in Service now. Sending to repo.");
            return _repo.Update(id, payload);
        }

        public Task Delete(Guid id)
        {
            return _repo.Delete(id);
        }
        public Task ChangeUserRole(Guid id, UserRole role)
        {
            return _repo.ChangeUserRole(id, role);
        }
        public Task ChangeUserStatus(Guid id, UserStatus status)
        {
            return _repo.ChangeUserStatus(id, status);
        }
        public Task AssignUserScopes(Guid id, List<string> scopes)
        {
            return _repo.AssignUserScopes(id, scopes);
        }
        public Task AddUserScopes(Guid id, List<string> scopes)
        {
            return _repo.AddUserScopes(id, scopes);
        }
    }
}
