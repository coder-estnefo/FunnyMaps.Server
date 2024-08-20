using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FunnyMaps.Server.Models
{
    public class User : IdentityUser
    {
        public List<Joke> Jokes { get; set; }
    }
}
