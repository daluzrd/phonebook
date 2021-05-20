using DataAccess;
using Models;
using System;
using System.Linq;

namespace DataServices
{
    public class UserService : CommonService<User>, IUserService
    {
        private readonly IHashingService _hashingService;
        public UserService(DataContext dbContext, IHashingService hashingService) : base(dbContext)
        {
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

            if(context.Users.Where(u => u.Username == username).Any())
                throw new Exception("username already exists.");

            password = _hashingService.Hashing(password);

            User user = new User();
            user.Username = username;
            user.Password = password;

            context.Add(user);
            context.SaveChanges();

            return user;
        }
    }
}