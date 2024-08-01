
using FunnyMaps.Server.Models;
using FunnyMaps.Server.Requests;

namespace FunnyMaps.Server.Response
{
    public class UserResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; } = null!;

        public UserResponse(User user)
        {
            Id = user.Id;
            Email = user.Email;
        }
    }
}
