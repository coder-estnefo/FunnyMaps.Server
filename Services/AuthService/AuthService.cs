using FunnyMaps.Server.Data;
using FunnyMaps.Server.Exceptions;
using FunnyMaps.Server.Models;
using FunnyMaps.Server.Requests;
using FunnyMaps.Server.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FunnyMaps.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AuthService> _logger;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<UserResponse> Register(UserRequest request)
        {
            var user = request.ToUser(request);

            var userResponse = await _userManager.CreateAsync(user,request.Password);

            if (!userResponse.Succeeded)
            {
                var error = userResponse.Errors.FirstOrDefault();
                throw new Exception(error?.Description);
            }

           var roleResponse = await _userManager.AddToRoleAsync(user, ApplicationRole.User);

            if (!roleResponse.Succeeded)
            {
                var error = roleResponse.Errors.FirstOrDefault();
                throw new Exception(error?.Description);
            }

            return new UserResponse(user);
        }

        public async Task<string> Login(UserRequest request)

        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, request.Password))
                {
                    var token = CreateToken(user);
                    return token;
                }

                throw new InvalidLoginDetailsException("Invalid Password!");
                
            }

            throw new InvalidLoginDetailsException("Invalid User!");
        }
    }
}
