using Microsoft.EntityFrameworkCore;
using Wayplot_Backend.Constants;
using Wayplot_Backend.Database;
using Wayplot_Backend.DTOs;
using Wayplot_Backend.Models;

namespace Wayplot_Backend.Repositories
{
    public class MapRepository : IMapRepository
    {
        private readonly WayplotDbContext _db;
        private readonly IAnalyticRepository _analyticRepository;

        public MapRepository(WayplotDbContext db, IAnalyticRepository analyticRepository)
        {
            _db = db;
            _analyticRepository = analyticRepository;
        }

        public async Task<List<MapWithAnalyticsDto>> GetAllMaps()
        {
            var result = await _db.Maps
            .Where(m => m.Status != MapStatus.DELETED)
            .GroupJoin(
                _db.AnalyticRecords,
                m => m.Id,
                a => a.EntityId,
                (m, analytics) => new MapWithAnalyticsDto
                {
                    Map = m,
                    ViewCount = analytics.Count(a => a.Type == "view"),
                    DownloadCount = analytics.Count(a => a.Type == "download")
                })
            .ToListAsync();

            Console.WriteLine("Adding view record to all maps.");
            foreach (var map in result)
            {
                await _analyticRepository.AddRecord("view", Guid.Empty, map.Map.Id);
            }

            return result;
        }

        public async Task<MapWithAnalyticsDto?> GetMap(Guid id)
        {
            var map = await _db.Maps
            .Where(m => m.Id == id && m.Status != MapStatus.DELETED)
            .GroupJoin(
                _db.AnalyticRecords,
                m => m.Id,
                a => a.EntityId,
                (m, analytics) => new MapWithAnalyticsDto
                {
                    Map = m,
                    ViewCount = analytics.Count(a => a.Type == "view"),
                    DownloadCount = analytics.Count(a => a.Type == "download")
                })
            .FirstOrDefaultAsync();

            if (map != null)  await _analyticRepository.AddRecord("view", Guid.Empty, map.Map.Id);

            return map;

        }

        public async Task<GetMapUrlResponseDTO?> GetMapURL(Guid id)
        {
            MapWithAnalyticsDto? map = await GetMap(id);
            if (map == null)
            {
                return null;
            }

            await _analyticRepository.AddRecord("view", Guid.Empty, id);

            return new GetMapUrlResponseDTO
            {
                GpxUrl = map.Map.GpxUrl,
                JsonUrl = map.Map.JsonUrl
            };
        }

        public async Task<Map> CreateMap(Guid uploadederId, CreateMapDTO uploadMapDTO)
        {
            Map newMap = new Map
            {
                Id = Guid.NewGuid(),
                Name = uploadMapDTO.Name,
                Description = uploadMapDTO.Description,
                UploadedBy = uploadederId,
                GpxUrl = uploadMapDTO.GpxUrl,
                JsonUrl = uploadMapDTO.JsonUrl,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Status = uploadMapDTO.Status ?? MapStatus.ACTIVE,
                Visibility = uploadMapDTO.Visibility ?? MapVisibility.PRIVATE
            };

            _db.Maps.Add(newMap);
            await _db.SaveChangesAsync();
            await _analyticRepository.AddRecord("create", uploadederId, newMap.Id);
            return newMap;
        }

        public async Task<Map?> EditMap(Guid id, EditMapDTO editMapDTO)
        {
            MapWithAnalyticsDto? map = await GetMap(id);
            if (map == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(editMapDTO.Name))
                map.Map.Name = editMapDTO.Name;

            if (!string.IsNullOrEmpty(editMapDTO.Description))
                map.Map.Description = editMapDTO.Description;

            if (!string.IsNullOrEmpty(editMapDTO.GpxUrl))
                map.Map.GpxUrl = editMapDTO.GpxUrl;

            if (!string.IsNullOrEmpty(editMapDTO.JsonUrl))
                map.Map.JsonUrl = editMapDTO.JsonUrl;

            if (editMapDTO.Status.HasValue)
                map.Map.Status = editMapDTO.Status.Value;

            if (editMapDTO.Visibility.HasValue)
                map.Map.Visibility = editMapDTO.Visibility.Value;

            map.Map.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return map.Map;
        }

        public async Task<Boolean> DeleteMap(Guid id)
        {
            MapWithAnalyticsDto? map = await GetMap(id);
            if (map == null)
            {
                return false;
            }

            map.Map.Status = MapStatus.DELETED;
            map.Map.UpdatedAt = DateTime.UtcNow;
            _db.Maps.Update(map.Map);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Map?> ChangeVisibility(Guid id, MapVisibility visibility)
        {
            MapWithAnalyticsDto? map = await GetMap(id);
            if (map == null)
            {
                return null;
            }

            map.Map.Visibility = visibility;
            map.Map.UpdatedAt = new DateTime();
            await _db.SaveChangesAsync();

            return map.Map;
        }

        public async Task<Map?> ChangeStatus(Guid id, MapStatus status)
        {
            MapWithAnalyticsDto? map = await GetMap(id);
            if (map == null)
            {
                return null;
            }

            map.Map.Status = status;
            map.Map.UpdatedAt = new DateTime();
            await _db.SaveChangesAsync();

            return map.Map;
        }

        public async Task<bool> LogMapDownload(Guid actorId, Guid mapId)
        {
            await _analyticRepository.AddRecord("download", actorId, mapId);
            return true;
        } 
    }
}
