using Healthcare.Application.DTOs.Enums;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.Services.Login;
using Healthcare.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Healthcare.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IRegisterService _registerService;
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;

        public AuthController(
            IRegisterService registerService,
            ILoginService loginService,
            ITokenService tokenService)
        {
            _registerService = registerService;
            _loginService = loginService;
            _tokenService = tokenService;
        }

        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            if (request.Rol == RolUsuarioDto.Admin)
            {
                var currentUserRole = User.FindFirstValue(ClaimTypes.Role);
                if (currentUserRole != RolUsuario.Admin.ToString())
                    return StatusCode(403, "Solo un admin puede crear otro admin.");
            }

            var result = await _registerService.RegisterAsync(request);
            if (!result)
                return BadRequest("User already exists.");

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var user = await _loginService.ValidateUserAsync(request);
            if (user == null)
                return Unauthorized("Invalid credentials.");

            var token = _tokenService.GenerateToken(user);

            return Ok(new { token });
        }
    }
}
