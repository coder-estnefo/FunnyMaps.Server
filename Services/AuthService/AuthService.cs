using FunnyMaps.Server.Data;
using FunnyMaps.Server.Exceptions;
using FunnyMaps.Server.Models;
using FunnyMaps.Server.Requests;
using FunnyMaps.Server.Response;
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
        private readonly DataContext _db;
        private readonly IConfiguration _configuration;

        public AuthService(DataContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }


        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
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

        public async Task<UserResponse> Register(UserRequest user)
        {
            var _user = _db.Users.FirstOrDefault(u => u.Email == user.Email);

            if (_user != null)
            {
                throw new UserExistsException("User already exists");
            }

            CreatePasswordHash(user.Password, out byte[] passwordHash,
                out byte[] passwordSalt);

            var newUser = new User
            {
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Email = user.Email,
            };


            var result = await _db.Users.AddAsync(newUser);
            _db.SaveChanges();

            return new UserResponse(result.Entity);
        }

        public async Task<string> Login([FromQuery] string email, [FromQuery] string password)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                var isPasswordValid = VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
                if (isPasswordValid)
                {
                    var token = CreateToken(user);
                    return token;
                }
                else
                {
                    throw new InvalidLoginDetailsException("Invalid Password");
                }
            }

            throw new InvalidLoginDetailsException("Invalid Login Details");
        }
    }
}
