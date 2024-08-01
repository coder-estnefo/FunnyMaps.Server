using System.ComponentModel.DataAnnotations;

namespace FunnyMaps.Server.Models
{
    public class Location
    {
        [Key]
        public int JokeId { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }
        public string Place { get; set; } = null!;
        public Joke Joke { get; set; }
    }
}
