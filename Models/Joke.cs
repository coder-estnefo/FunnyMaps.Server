using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FunnyMaps.Server.Models
{
    public class Joke
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Description { get; set; } = null!;
        public Location Location { get; set; } = null!;
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
