using DataAccess;
using Models;
using System;
using System.Linq;

namespace DataServices
{
    public class UserService : CommonService<User>
    {
        private readonly DataContext _context;
        private readonly HashingService _hashingService;
        public UserService(DataContext dbContext, HashingService hashingService) : base(dbContext)
        {
            _context = dbContext;
            _hashingService = hashingService;
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

            password = _hashingService.Hashing(password);

            User user = new User();
            user.Username = username;
            user.Password = password;

            _context.Add(user);
            _context.SaveChanges();

            return user;
        }
    }
}