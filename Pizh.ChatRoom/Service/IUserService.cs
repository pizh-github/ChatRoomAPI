using Pizh.ChatRoom.Models;

namespace Pizh.ChatRoom.Service
{
    public interface IUserService
    {
        Task<int> Register(string name, string password);

        Task<int> Login(string name, string password);
    }
}
