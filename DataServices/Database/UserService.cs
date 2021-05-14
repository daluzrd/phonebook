using System;
using System.Security.Cryptography;
using System.Text;
using DataAccess;
using Models;

namespace DataServices
{
    public class UserService : CommonService<User>
    {
        private readonly DataContext _context;
        public UserService(DataContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public User Post(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("username is required.");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("password is required.");
            if (password.Length < 8)
                throw new ArgumentException("password should have 8 or more characters.");

            SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            
            password = string.Empty;
            foreach (var item in bytes)
            {
                password+=string.Format("{0:x2}", item);
            }

            User user = new User();
            user.Username = username;
            user.Password = password;

            _context.Add(user);
            _context.SaveChanges();

            return user;
        }
    }
}