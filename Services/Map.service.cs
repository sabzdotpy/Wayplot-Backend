using Wayplot_Backend.Constants;
using Wayplot_Backend.DTOs;
using Wayplot_Backend.Repositories;
using Wayplot_Backend.Models;

namespace Wayplot_Backend.Services
{
    public sealed class MapService : IMapService
    {
        private readonly IMapRepository _repo;

        public MapService(IMapRepository repo)
        {
            _repo = repo;
        }

        public async Task<MapResponseDTO> GetAll()
        {
            try
            {
                List<Map>? maps = await _repo.GetAllMaps();

                if (maps == null || maps.Count == 0)
                {
                    return new MapResponseDTO
                    {
                        IsError = true,
                        IsSuccess = false,
                        ErrorMessage = "",
                        Message = "Error in fetching all maps."
                    };
                }

                return new MapResponseDTO
                {
                    IsError = false,
                    IsSuccess = true,
                    data = maps,
                    Message = "Successfully fetched all maps."
                };
            }
            catch (Exception e)
            {
                return new MapResponseDTO
                {
                    IsError = true,
                    IsSuccess = false,
                    data = null,
                    Message = "Error in fetching all maps.",
                    ErrorMessage = e.Message
                };
            }
        }

        public async Task<MapResponseDTO> ChangeStatus(Guid id, MapStatus status)
        {
            try
            {
                Map? updatedMap = await _repo.ChangeStatus(id, status);

                if (updatedMap == null)
                {
                    return new MapResponseDTO
                    {
                        IsError = true,
                        IsSuccess = false,
                        ErrorMessage = "",
                        Message = "Error in updating map."
                    };
                }

                return new MapResponseDTO
                {
                    IsError = false,
                    IsSuccess = true,
                    data = updatedMap,
                    Message = "Successfully updated map."
                };
            }
            catch (Exception e)
            {
                return new MapResponseDTO
                {
                    IsError = true,
                    IsSuccess = false,
                    data = null,
                    Message = "Error in updating map.",
                    ErrorMessage = e.Message
                };
            }
        }

        public async Task<MapResponseDTO> ChangeVisibility(Guid id, MapVisibility visibility)
        {
            try
            {
                Map? updatedMap = await _repo.ChangeVisibility(id, visibility);
                if (updatedMap == null)
                {
                    return new MapResponseDTO
                    {
                        IsError = true,
                        IsSuccess = false,
                        ErrorMessage = "",
                        Message = "Error in changing visibility of map."
                    };
                }

                return new MapResponseDTO
                {
                    IsError = false,
                    IsSuccess = true,
                    data = updatedMap,
                    Message = "Successfully changed visibility of map."
                };
            }
            catch (Exception e)
            {
                return new MapResponseDTO
                {
                    IsError = true,
                    IsSuccess = false,
                    data = null,
                    Message = "Error in changing visibility of map.",
                    ErrorMessage = e.Message
                };
            }
        }

        public async Task<MapResponseDTO> DeleteMap(Guid id)
        {
            try
            {
                bool deleted = await _repo.DeleteMap(id);
                if (deleted)
                {
                    return new MapResponseDTO
                    {
                        IsError = false,
                        IsSuccess = true,
                        Message = "Successfully deleted map."
                    };
                }

                return new MapResponseDTO
                {
                    IsError = true,
                    IsSuccess = false,
                    Message = "Error in deleting map."
                };
            }
            catch (Exception e)
            {
                return new MapResponseDTO
                {
                    IsError = true,
                    IsSuccess = false,
                    data = null,
                    Message = "Error in deleting map.",
                    ErrorMessage = e.Message
                };
            }
        }

        public async Task<MapResponseDTO> EditMap(Guid id, EditMapDTO editMapDTO)
        {
            try
            {
                Map? updatedMap = await _repo.EditMap(id, editMapDTO);
                if (updatedMap == null)
                {
                    return new MapResponseDTO
                    {
                        IsError = true,
                        IsSuccess = false,
                        Message = "Error in editing map."
                    };
                }

                return new MapResponseDTO
                {
                    IsError = false,
                    IsSuccess = true,
                    Message = "Successfully edited map.",
                    data = updatedMap
                };
            }
            catch (Exception e)
            {
                return new MapResponseDTO
                {
                    IsError = true,
                    IsSuccess = false,
                    data = null,
                    Message = "Error in editing map.",
                    ErrorMessage = e.Message
                };
            }
        }

        public async Task<MapResponseDTO> GetMap(Guid id)
        {
            try
            {
                Map? map = await _repo.GetMap(id);
                if (map == null)
                {
                    return new MapResponseDTO
                    {
                        IsError = true,
                        IsSuccess = false,
                        Message = "Error in getting map."
                    };
                }

                return new MapResponseDTO
                {
                    IsError = false,
                    IsSuccess = true,
                    Message = "Successfully retrieved map.",
                    data = map
                };
            }
            catch (Exception e)
            {
                return new MapResponseDTO
                {
                    IsError = true,
                    IsSuccess = false,
                    data = null,
                    Message = "Error in getting map.",
                    ErrorMessage = e.Message
                };
            }
        }

        public async Task<MapResponseDTO> GetMapURL(Guid id)
        {
            try
            {
                GetMapUrlResponseDTO? urlResponse = await _repo.GetMapURL(id);
                if (urlResponse == null)
                {
                    return new MapResponseDTO
                    {
                        IsError = true,
                        IsSuccess = false,
                        Message = "Error in getting map URL."
                    };
                }

                return new MapResponseDTO
                {
                    IsError = false,
                    IsSuccess = true,
                    Message = "Successfully retrieved map urls.",
                    data = urlResponse
                };
            }
            catch (Exception e)
            {
                return new MapResponseDTO
                {
                    IsError = true,
                    IsSuccess = false,
                    data = null,
                    Message = "Error in getting map.",
                    ErrorMessage = e.Message
                };
            }
        }

        public async Task<MapResponseDTO> CreateMap(Guid uploaderId, CreateMapDTO uploadMapDTO)
        {
            try
            {
                Map? map = await _repo.CreateMap(uploaderId, uploadMapDTO);
                if (map == null)
                {
                    return new MapResponseDTO
                    {
                        IsError = true,
                        IsSuccess = false,
                        Message = "Error in creating map."
                    };
                }

                return new MapResponseDTO
                {
                    IsError = false,
                    IsSuccess = true,
                    Message = "Successfully created map.",
                    data = map
                };
            }
            catch (Exception e)
            {
                return new MapResponseDTO
                {
                    IsError = true,
                    IsSuccess = false,
                    data = null,
                    Message = "Error in craeting map.",
                    ErrorMessage = e.Message
                };
            }
        }
    }
}
