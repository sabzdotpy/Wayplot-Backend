using Microsoft.AspNetCore.Mvc;
using Wayplot_Backend.Constants;
using Wayplot_Backend.DTOs;
using Wayplot_Backend.Services;

namespace Wayplot_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService service)
        {
            _authService = service;
        }

        [HttpGet("get-auth-types")]
        public IActionResult GetAuthTypes()
        {
            var values = Enum.GetNames(typeof(AuthType)).ToList();
            return Ok(values);
        }


        [HttpGet("get-user-roles")]
        public IActionResult GetUserRoles()
        {
            var values = Enum.GetNames(typeof(UserRole)).ToList();
            return Ok(values);
        }


        [HttpPost("login")]
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

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] LoginRequestDto request)
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
