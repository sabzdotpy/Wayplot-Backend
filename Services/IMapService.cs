using Wayplot_Backend.Constants;
using Wayplot_Backend.DTOs;

namespace Wayplot_Backend.Services
{
    public interface IMapService
    {
        Task<MapResponseDTO> GetMap(Guid id);
        Task<MapResponseDTO> GetMapURL(Guid id);
        Task<MapResponseDTO> CreateMap(Guid creatorId, CreateMapDTO uploadMapDTO);
        Task<MapResponseDTO> EditMap(Guid id, EditMapDTO editMapDTO);
        Task<MapResponseDTO> DeleteMap(Guid id);
        Task<MapResponseDTO> ChangeVisibility(Guid id, MapVisibility visibility);
        Task<MapResponseDTO> ChangeStatus(Guid id, MapStatus status);
    }
}
