using Pizh.ChatRoom.Models;

namespace Pizh.ChatRoom.Service
{
    public interface IUserService
    {
        int Register(string name, string password);

        int Login(string name, string password);
    }
}
