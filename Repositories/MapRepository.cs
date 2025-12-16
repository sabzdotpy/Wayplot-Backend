using Microsoft.EntityFrameworkCore;
using Wayplot_Backend.DTOs;
using Wayplot_Backend.Models;
using Wayplot_Backend.Constants;

namespace Wayplot_Backend.Repositories
{
    public class MapRepository : IMapRepository
    {
        private readonly Database.WayplotDbContext _db;

        public MapRepository(Database.WayplotDbContext db)
        {
            _db = db;
        }

        public async Task<Map?> GetMap(Guid id)
        {
            return await _db.Maps.FirstOrDefaultAsync(map => map.Id == id);
            
        }

        public async Task<GetMapUrlResponseDTO?> GetMapURL(Guid id)
        {
            Map? map = await GetMap(id);
            if (map == null)
            {
                return null;
            }

            return new GetMapUrlResponseDTO
            {
                GpxUrl = map.GpxUrl,
                JsonUrl = map.JsonUrl
            };
        }

        public async Task<Map> CreateMap(Guid uploadederId, CreateMapDTO uploadMapDTO)
        {
            Map newMap = new Map
            {
                Id = new Guid(),
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
            return newMap;
        }

        public async Task<Map?> EditMap(Guid id, EditMapDTO editMapDTO)
        {
            Map? map = await GetMap(id);
            if (map == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(editMapDTO.Name))
                map.Name = editMapDTO.Name;

            if (!string.IsNullOrEmpty(editMapDTO.Description))
                map.Description = editMapDTO.Description;

            if (!string.IsNullOrEmpty(editMapDTO.GpxUrl))
                map.GpxUrl = editMapDTO.GpxUrl;

            if (!string.IsNullOrEmpty(editMapDTO.JsonUrl))
                map.JsonUrl = editMapDTO.JsonUrl;

            if (editMapDTO.Status.HasValue)
                map.Status = editMapDTO.Status.Value;

            if (editMapDTO.Visibility.HasValue)
                map.Visibility = editMapDTO.Visibility.Value;

            map.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return map;
        }

        public async Task<Boolean> DeleteMap(Guid id)
        {
            Map? map = await GetMap(id);
            if (map == null)
            {
                return false;
            }

            map.Status = MapStatus.DELETED;
            map.UpdatedAt = new DateTime();
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Map?> ChangeVisibility(Guid id, MapVisibility visibility)
        {
            Map? map = await GetMap(id);
            if (map == null)
            {
                return null;
            }

            map.Visibility = visibility;
            map.UpdatedAt = new DateTime();
            await _db.SaveChangesAsync();

            return map;
        }

        public async Task<Map?> ChangeStatus(Guid id, MapStatus status)
        {
            Map? map = await GetMap(id);
            if (map == null)
            {
                return null;
            }

            map.Status = status;
            map.UpdatedAt = new DateTime();
            await _db.SaveChangesAsync();

            return map;
        }
    }
}
