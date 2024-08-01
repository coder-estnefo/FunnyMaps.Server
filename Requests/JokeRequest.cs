namespace FunnyMaps.Server.Requests
{
    public class JokeRequest
    { 
        public string Description { get; set; } = null!;
        public LocationRequest Location { get; set; } = null!;
    }
}
