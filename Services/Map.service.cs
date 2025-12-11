using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Wayplot_Backend.Constants;
using Wayplot_Backend.DTOs;
using Wayplot_Backend.Models;
using Wayplot_Backend.Repositories;

namespace Wayplot_Backend.Services
{
    public sealed class MapService : IMapService
    {
        private readonly IAuthRepository _repo;

        public MapService(IAuthRepository repo)
        {
            _repo = repo;
        }

        public Task<MapResponseDTO> ChangeStatus(Guid id, MapStatus status)
        {
            throw new NotImplementedException();
        }

        public Task<MapResponseDTO> ChangeVisibility(Guid id, MapVisibility visibility)
        {
            throw new NotImplementedException();
        }

        public Task<MapResponseDTO> DeleteMap(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<MapResponseDTO> EditMap(Guid id, EditMapDTO editMapDTO)
        {
            throw new NotImplementedException();
        }

        public Task<MapResponseDTO> GetMap(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<MapResponseDTO> GetMapURL(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<MapResponseDTO> CreateMap(CreateMapDTO uploadMapDTO)
        {
            throw new NotImplementedException();
        }
    }
}
