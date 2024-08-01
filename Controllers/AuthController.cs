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
        public async Task<ActionResult> Register(UserRequest user)
        {
            var newUser = await _authService.Register(user);
            if (newUser == null)
            {
                return BadRequest("User not created");
            }

            var message = "User created";

            return Ok(new { message });
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(string username, string password)
        {
            string token = await _authService.Login(username, password);

            return Ok(token);
        }
    }
}
