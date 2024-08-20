using FunnyMaps.Server.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FunnyMaps.Server.Requests
{
    public class UserRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required, StringLength(64,MinimumLength =8)]
        public string Password { get; set; } = null!;

        public User ToUser(UserRequest request)
        {
            return new User
            {
                Email = request.Email,
                UserName = request.Email,
            };
        }
    }

}
