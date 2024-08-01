namespace FunnyMaps.Server.Requests
{
    public class LocationRequest
    {
        public long Latitude { get; set; }
        public long Longitude { get; set; }
        public string Place { get; set; } = null!;
    }
}
