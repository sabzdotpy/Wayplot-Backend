using Microsoft.AspNetCore.Mvc;
using Wayplot_Backend.DTOs;
using Wayplot_Backend.Services;

namespace Wayplot_Backend.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public sealed class MapController : ControllerBase
    {
        private readonly IMapService _mapService;

        public MapController(IMapService service)
        {
            _mapService = service;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetMap(Guid id)
        {
            MapResponseDTO res = await _mapService.GetMap(id);
            if (res.IsError)
            {
                return NotFound(res.ErrorMessage);
            }

            return Ok(new
            {
                id = id,
                data = res.data,
                message = res.Message
            });
        }


        [HttpGet("url/{id:guid}")]
        public async Task<IActionResult> GetMapURL(Guid id)
        {
            MapResponseDTO res = await _mapService.GetMapURL(id);
            if (res.IsError)
            {
                return NotFound(res);
            }

            return Ok(res);
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] CreateMapDTO request)
        {

            MapResponseDTO res = await _mapService.CreateMap(request);
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest(res);
            }
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> EditMap(Guid id, [FromBody] EditMapDTO request)
        {
            MapResponseDTO res = await _mapService.EditMap(id, request);
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest(res);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteMap(Guid id)
        {
            MapResponseDTO res = await _mapService.DeleteMap(id);
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest(res);
            }
        }

        [HttpPost("change-visibility/{id:guid}")]
        public async Task<IActionResult> ChangeVisibility(Guid id, [FromBody] ChangeMapVisibilityDTO visibilityDTO )
        {
            MapResponseDTO res = await _mapService.ChangeVisibility(id, visibilityDTO.visibility);
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest(res);
            }
        }

        [HttpPost("change-status/{id:guid}")]
        public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] ChangeMapStatusDTO statusDTO)
        {
            MapResponseDTO res = await _mapService.ChangeStatus(id, statusDTO.status);
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest(res);
            }
        }
    }
}
