using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DataAccess;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using Models;

namespace DataServices
{
    public class UserService : CommonService<User>
    {
        private readonly AppSettings _appSettings;
        private readonly DataContext _context;
        public UserService(DataContext dbContext, IOptions<AppSettings> appSettings) : base(dbContext)
        {
            _context = dbContext;
            _appSettings = appSettings.Value;
        }

        public string Hashing(string password)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            
            password = string.Empty;
            foreach (var item in bytes)
            {
                password+=string.Format("{0:x2}", item);
            }

            return password;
        }

        public string GenerateToken(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.PrivateKey);
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

            password = Hashing(password);

            User user = _context.Users.Where(u => u.Username == username && u.Password == password).FirstOrDefault();

            if(user != null)
                return new { token = GenerateToken(user) };
            throw new Exception();
        }



        public User Post(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("username is required.");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("password is required.");
            if (password.Length < 8)
                throw new ArgumentException("password should have 8 or more characters.");

            if(_context.Users.Where(u => u.Username == username).Any())
                throw new Exception("username already exists.");

            password = Hashing(password);

            User user = new User();
            user.Username = username;
            user.Password = password;

            _context.Add(user);
            _context.SaveChanges();

            return user;
        }
    }
}