using Wayplot_Backend.Constants;
using Wayplot_Backend.DTOs;
using Wayplot_Backend.Models;

namespace Wayplot_Backend.Repositories
{
    public interface IMapRepository
    {
        Task<List<MapWithAnalyticsDto>> GetAllMaps();
        Task<MapWithAnalyticsDto?> GetMap(Guid id);
        Task<bool> LogMapDownload(Guid actorId, Guid mapId);
        Task<GetMapUrlResponseDTO?> GetMapURL(Guid id);
        Task<Map> CreateMap(Guid uploadederId, CreateMapDTO uploadMapDTO);
        Task<Map?> EditMap(Guid id, EditMapDTO editMapDTO);
        Task<Boolean> DeleteMap(Guid id);
        Task<Map?> ChangeVisibility(Guid id, MapVisibility visibility);
        Task<Map?> ChangeStatus(Guid id, MapStatus status);
    }
}
