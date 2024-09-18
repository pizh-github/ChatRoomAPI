using Pizh.ChatRoom.Models;
using SqlSugar;
using System.Reflection;

namespace Pizh.ChatRoom.Service
{
    public class UserService : IUserService
    {
        private readonly ISqlSugarClient _db;
        public UserService(ISqlSugarClient db)
        {
            _db = db;
        }

        public async Task<int> Login(string name, string password)
        {
            UserInfo user = await GetUser(name);
            
            if (user == null)
            {
                return Constants.LoginConst.UserNotExist;
            }
            else if (user.Password != password)
            {
                return Constants.LoginConst.PasswordError;
            }
            else
            {
                return Constants.LoginConst.OK;
            }

        }
        public async Task<int> Register(string name, string password)
        {
            UserInfo user = await GetUser(name);
            if (user == null)
            {
                UserInfo newInfo = new UserInfo()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name,
                    Password = password
                };
                return _db.Insertable(newInfo).ExecuteCommand() == 0 ? Constants.RegisterConst.Error : Constants.RegisterConst.OK;
            }
            else
            {
                return Constants.RegisterConst.UserAlreadyExist;
            }
        }
        public async Task<UserInfo> GetUser(string name)
        {
            return await Task.Run(() => _db.Queryable<UserInfo>().First(x => x.Name == name));
        }
    }
}
