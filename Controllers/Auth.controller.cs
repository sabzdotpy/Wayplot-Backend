using Microsoft.AspNetCore.Mvc;
using Wayplot_Backend.DTOs;
using Wayplot_Backend.Services;

namespace Wayplot_Backend.Controllers
{
    [ApiController]
    [Route("[controller]/login")]
    public sealed class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService service)
        {
            _authService = service;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            string email = request.Email;
            string password = request.Password;

            LoginResponseDto res = await _authService.Login(email, password);
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
