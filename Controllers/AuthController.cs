using FunnyMaps.Server.Requests;
using FunnyMaps.Server.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace FunnyMaps.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> Register(UserRequest request)
        {
            var user = await _authService.Register(request);

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserRequest request)
        {
            string token = await _authService.Login(request);

            return Ok(new { value = token});
        }
    }
}
