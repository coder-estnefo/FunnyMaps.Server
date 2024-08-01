using FunnyMaps.Server.Models;
using FunnyMaps.Server.Requests;
using FunnyMaps.Server.Response;

namespace FunnyMaps.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<UserResponse> Register(UserRequest user);
        Task<string> Login(string username, string password);
        abstract void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        string CreateToken(User user);
    }
}
