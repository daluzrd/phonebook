using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace DataServices
{
    public class AuthenticationService
    {
        private readonly HashingService _hashingService;
        private readonly string _privateKey;
        private readonly UserService _userService;
        public AuthenticationService(UserService userService, HashingService hashingService, IConfiguration configuration)
        {
            _userService = userService;
            _hashingService = hashingService;
            _privateKey = configuration.GetValue<string>("PrivateKey");
        }

        public string GenerateToken(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_privateKey);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public dynamic Login(string username, string password)
        {
            if(string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("username is required.");

            if(string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("password is required.");

            password = _hashingService.Hashing(password);

            User user = _userService.GetByFilter(u => u.Username == username && u.Password == password).FirstOrDefault();

            if(user == null)
                throw new Exception();
            
            return new { token = GenerateToken(user) };
        }

    }
}