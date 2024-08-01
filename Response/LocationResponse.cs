namespace FunnyMaps.Server.Response
{
    public class LocationResponse
    {
        public long Latitude { get; set; }
        public long Longitude { get; set; }
        public string Place { get; set; } = null!;
    }
}
