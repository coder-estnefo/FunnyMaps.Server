using FunnyMaps.Server.Data;
using FunnyMaps.Server.Models;
using FunnyMaps.Server.Requests;
using FunnyMaps.Server.Response;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FunnyMaps.Server.Services.JokeService
{
    public class JokeService : IJokeService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _db;

        public JokeService(IHttpContextAccessor httpContextAccessor, DataContext db)
        {
            _httpContextAccessor = httpContextAccessor;
            _db = db;
        }

        public async Task<JokeResponse> AddJoke(JokeRequest request)
        {
            //if (_httpContextAccessor.HttpContext?.User != null)
            {
                var claims = _httpContextAccessor.HttpContext.User;
                var email = claims.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

                    if (user != null)
                    {
                        var location = new Location
                        {
                            Place = request.Location.Place,
                            Longitude = request.Location.Longitude,
                            Latitude = request.Location.Latitude,
                        };

                        var joke = new Joke()
                        {
                            Description = request.Description,
                            Location = location,
                            UserId = user.Id,
                        };

                        var response = await _db.Jokes.AddAsync(joke);
                        await _db.SaveChangesAsync();

                        //return new JokeResponse()
                        //{
                        //    Id = response.Entity.Id,
                        //    Description = response.Entity.Description,
                        //    Location = response.Entity.Location,
                        //};

                        var locationRequest = new LocationResponse()
                        {
                            Latitude = request.Location.Latitude,
                            Longitude = request.Location.Longitude,
                            Place = request.Location.Place,
                        };

                        var jokeResponse = new JokeResponse()
                        {
                            Id =joke.Id,
                            Description = joke.Description,
                            Location = locationRequest,
                        };

                        return jokeResponse;
                    }

                }

                throw new Exception("Unable to add joke");

            }
            
        }

        public async Task<List<JokeResponse>> GetJokesByLocation(string location)
        {
            List<JokeResponse> jokes = new();

            var _jokes = await _db.Jokes
                .Where(j => j.Location.Place == location)
                .Include(j => j.Location)
                .ToListAsync();

            foreach (var joke in _jokes)
            {
                Console.WriteLine($"{joke.Id} {joke.Description}");
                var locationResponse = new LocationResponse()
                {
                    Latitude = joke.Location.Latitude,
                    Longitude = joke.Location.Longitude,
                    Place = joke.Location.Place,
                };

                var responseJoke = new JokeResponse()
                {
                    Id = joke.Id,
                    Description = joke.Description,
                    Location = locationResponse,
                };


                jokes.Add(responseJoke);
            }

            return jokes;
        }
    }
}
