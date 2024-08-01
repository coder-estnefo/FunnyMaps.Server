using FunnyMaps.Server.Requests;
using FunnyMaps.Server.Response;

namespace FunnyMaps.Server.Services.JokeService
{
    public interface IJokeService
    {
        Task<JokeResponse> AddJoke(JokeRequest request);
        Task<List<JokeResponse>> GetJokesByLocation(string location);
    }
}
