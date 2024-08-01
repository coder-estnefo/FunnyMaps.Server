using Microsoft.EntityFrameworkCore;

namespace FunnyMaps.Server.Models
{
    public class Joke
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Description { get; set; } = null!;
        public Location Location { get; set; } = null!;
        public Guid UserId { get; set; }
    }
}
