using FunnyMaps.Server.Requests;
using FunnyMaps.Server.Response;
using FunnyMaps.Server.Services.JokeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FunnyMaps.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokesController : ControllerBase
    {
        private readonly IJokeService _jokeService;

        public JokesController(IJokeService jokeService)
        {
            _jokeService = jokeService;
        }

        [HttpPost("AddJoke")]
        [Authorize]
        public async Task<ActionResult> PostJoke(JokeRequest joke)
        {
            var response = await _jokeService.AddJoke(joke);

            return Ok(response);
        }

        [HttpGet("GetJokesByLocation")]
        public async Task<ActionResult<List<JokeResponse>>> GetJokes(string location)
        {
            var jokes = await _jokeService.GetJokesByLocation(location);

            return Ok(jokes);
        }
    }
}
