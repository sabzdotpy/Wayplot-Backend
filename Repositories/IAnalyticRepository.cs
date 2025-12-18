using Wayplot_Backend.DTOs;

namespace Wayplot_Backend.Repositories
{
    public interface IAnalyticRepository
    {
        Task<bool> AddRecord(string type, Guid actorId, Guid entityId);
        //Task<>
    }
}
