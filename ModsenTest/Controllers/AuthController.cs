using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModsenTest.Dtos;
using ModsenTest.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ModsenTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    [SwaggerTag("This controller is used to manage authentication")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [SwaggerOperation(Summary = "Login to the system", Description = "Login to the system")]
        [SwaggerResponse(200, Type = typeof(string), Description = "200 OK : Returns the jwt-token of the author")]
        [SwaggerResponse(400, Description = "400 Bad Request : The request Body is not valid")]
        [SwaggerResponse(401, Description = "401 Unauthorized : The credentials are not valid")]
        public async Task<string> Login([FromBody] LoginDto loginDto)
        {
            return await _authService.Login(loginDto);
        }

        [HttpPost("register")]
        [SwaggerOperation(Summary = "Register to the system", Description = "Register to the system")]
        [SwaggerResponse(200, Type = typeof(RegisteredAuthorDto), Description = "200 OK : Returns the registered author")]
        [SwaggerResponse(400, Description = "400 Bad Request : The request Body is not valid")]
        [SwaggerResponse(409, Description = "409 Conflict : The name is already in use")]
        public async Task<IActionResult> Register([FromBody] RegisterAuthorDto registerAuthorDto)
        {
            return Ok(await _authService.Register(registerAuthorDto));
        }
    }
}
