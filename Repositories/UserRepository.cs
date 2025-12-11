using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Wayplot_Backend.DTOs;
using Wayplot_Backend.Models;
using Wayplot_Backend.Constants;

namespace Wayplot_Backend.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly Database.WayplotDbContext _db;

        public UserRepository(Database.WayplotDbContext db)
        {
            _db = db;
        }
        public async Task<List<User>> GetAll()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<List<User>> GetAllExcludeDeleted()
        {
            return await _db.Users.Where(u => u.Status != UserStatus.DELETED).ToListAsync();
        }

        public async Task<User?> Get(Guid id)
        {
            return await _db.Users.FindAsync(id);
        }

        public async Task Update(Guid id, UserUpdateDTO payload)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return;

            if (payload.Name != null) user.Name = payload.Name;
            if (payload.Email != null) user.Email = payload.Email;
            if (payload.Password != null) user.Password = payload.Password;
            if (payload.AuthType.HasValue) user.AuthType = payload.AuthType.Value;
            if (payload.Role.HasValue) user.Role = payload.Role.Value;
            if (payload.Status.HasValue) user.Status = payload.Status.Value;
            if (payload.Scopes != null) user.Scopes = payload.Scopes;

            user.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            User? user = await _db.Users.FindAsync(id);
            if (user == null) return;
            user.Status = UserStatus.DELETED;
            user.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }
        public async Task ChangeUserRole(Guid id, UserRole role)
        {
            User? user = await _db.Users.FindAsync(id);
            if (user == null) return;
            user.Role = role;
            user.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }
        public async Task ChangeUserStatus(Guid id, UserStatus status)
        {
            User? user = await _db.Users.FindAsync(id);
            if (user == null) return;
            user.Status = status;
            user.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }
    }
}