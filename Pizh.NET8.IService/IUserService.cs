using Pizh.NET8.Model;

namespace Pizh.NET8.IService
{
    public interface IUserService
    {
        //Task<List<UserVo>> Query();
        User Login(string username, string password);
        bool Register(UserVo userVo);

    }
}
