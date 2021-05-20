using Models;

namespace DataServices
{
    public interface IUserService : ICommonService<User>
    {
        User Post(string username, string password);
    }
}