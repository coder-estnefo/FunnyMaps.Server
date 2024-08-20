
using FunnyMaps.Server.Models;
using FunnyMaps.Server.Requests;

namespace FunnyMaps.Server.Response
{
    public class UserResponse
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;

        public UserResponse(User user)
        {
            Id = user.Id;
            Email = user.Email;
        }
    }
}
