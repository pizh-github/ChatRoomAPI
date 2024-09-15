using Pizh.NET8.Model;

namespace Pizh.NET8.Repository
{
    public interface IUserRepo
    {
        Task<List<User>> Query();

        User Login(string username, string password);

        bool Register(User user);

        bool UserExists(User user);
    }
}
