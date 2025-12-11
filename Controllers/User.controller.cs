using Microsoft.AspNetCore.Mvc;
using System.Data;
using Wayplot_Backend.Constants;
using Wayplot_Backend.DTOs;
using Wayplot_Backend.Services;


namespace Wayplot_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService service)
        {
            _userService = service;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAll());
        }

        [HttpGet("exclude-deleted")]
        public async Task<IActionResult> GetAllExcludeDeleted()
        {
            return Ok(await _userService.GetAllExcludeDeleted());
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var s = await _userService.Get(id);
            if (s == null) return NotFound();
            return Ok(s);
        }

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateDTO patch)
        {
            await _userService.Update(id, patch);
            return await Get(id);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.Delete(id);
            return Ok("User deleted successfully.");
        }

        [HttpPatch("{id:Guid}/change-role")]
        public async Task<IActionResult> ChangeRole(Guid id, [FromBody] ChangeRoleDTO roleDTO)
        {
            await _userService.ChangeUserRole(id, roleDTO.Role);
            return await Get(id);
        }

        [HttpPatch("{id:Guid}/change-status")]
        public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] ChangeStatusDTO statusDTO)
        {
            await _userService.ChangeUserStatus(id, statusDTO.Status);
            return await Get(id);
        }

        [HttpPost("{id:Guid}/assign-scopes")]
        public async Task<IActionResult> AssignScopes(Guid id, [FromBody] ChangeScopesDTO statusDTO)
        {
            await _userService.AssignUserScopes(id, statusDTO.Scopes);
            return await Get(id);
        }

        [HttpPost("{id:Guid}/add-scopes")]
        public async Task<IActionResult> AddScopes(Guid id, [FromBody] ChangeScopesDTO statusDTO)
        {
            await _userService.AddUserScopes(id, statusDTO.Scopes);
            return await Get(id);
        }
    }
}
