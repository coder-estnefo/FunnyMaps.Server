using FunnyMaps.Server.Requests;

namespace FunnyMaps.Server.Response
{
    public class JokeResponse
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public LocationResponse Location { get; set; } = null!;
    }
}
