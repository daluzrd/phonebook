using Models;

namespace DataServices
{
    public interface IAuthenticationService
    {
        string GenerateToken(User user);
        dynamic Login(string username, string password);
    }
}