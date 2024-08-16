namespace FunnyMaps.Server.Response
{
    public class LocationResponse
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Place { get; set; } = null!;
    }
}
