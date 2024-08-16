using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FunnyMaps.Server.Models
{
    public class Location
    {
        [Key]
        public int JokeId { get; set; }
        [Column(TypeName = "decimal(18,11)")]
        public decimal Latitude { get; set; }
        [Column(TypeName = "decimal(18,11)")]
        public decimal Longitude { get; set; }
        public string Place { get; set; } = null!;
        public Joke Joke { get; set; }
    }
}
