using Pizh.ChatRoom.Models;

namespace Pizh.ChatRoom.Service
{
    public interface IUserService
    {
        bool CodeFirst();

        int Register(string name, string password);

        int Login(string name, string password);

        List<UserMessage> GetMessages(int pageIndex, int PageSize);
    }
}
