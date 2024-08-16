namespace FunnyMaps.Server.Requests
{
    public class LocationRequest
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Place { get; set; } = null!;
    }
}
