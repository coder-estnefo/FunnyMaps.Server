using System.ComponentModel.DataAnnotations;

namespace FunnyMaps.Server.Models
{
    public class User
    {
        public Guid Id { get; set ; } = Guid.NewGuid();
        public string Email { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        //public List<Joke> Jokes { get; set; }
    }
}
