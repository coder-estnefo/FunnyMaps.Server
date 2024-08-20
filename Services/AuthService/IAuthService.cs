using FunnyMaps.Server.Models;
using FunnyMaps.Server.Requests;
using FunnyMaps.Server.Response;

namespace FunnyMaps.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<UserResponse> Register(UserRequest request);
        Task<string> Login(UserRequest request);
        string CreateToken(User user);
    }
}
