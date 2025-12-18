
using System.Runtime.CompilerServices;
using Wayplot_Backend.Database;
using Wayplot_Backend.Models;
using Wayplot_Backend.Utilities;

namespace Wayplot_Backend.Repositories
{
    public class AnalyticRepository : IAnalyticRepository
    {
        private readonly WayplotDbContext _db;
        public AnalyticRepository(WayplotDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddRecord(string type, Guid actorId, Guid entityId)
        {

            string[] validRecordTypes = { "view", "download", "create", "login", "signup" };

            if (!validRecordTypes.Contains(type))
            {
                throw new ArgumentException($"'type' must be one of {string.Join(',', validRecordTypes)}");
            }

            AnalyticRecord analyticRecord = new AnalyticRecord
            {
                Id = Guid.NewGuid(),
                ActorId = actorId,
                EntityId = entityId,
                Status = Constants.AnalyticRecordStatus.ACTIVE,
                Type = type,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            await _db.AnalyticRecords.AddAsync(analyticRecord);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
