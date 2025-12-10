using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAll());
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
    }
}
