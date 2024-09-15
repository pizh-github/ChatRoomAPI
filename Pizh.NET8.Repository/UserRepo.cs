using Newtonsoft.Json;
using Pizh.NET8.Model;
using Pizh.NET8.Repository.Base;

namespace Pizh.NET8.Repository
{
    public class UserRepo : IUserRepo
    {
        public async Task<List<User>> Query()
        {
            await Task.CompletedTask;
            List<User> users = PGSql.PG_db.Queryable<User>().ToList();

            //var data = "[{\"Id\":\"01\",\"Name\":\"Pizh\"}]";
            //return JsonConvert.DeserializeObject<List<User>>(data) ?? new List<User>();
            return users;
        }

        /// <summary>
        /// 登录查询
        /// </summary>
        /// <returns></returns>
        public User Login(string username, string password)
        {
            return PGSql.PG_db.Queryable<User>().First(u => u.Name == username && u.Password == password);
        }

        /// <summary>
        /// 注册新增
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Register(User user)
        {
           return PGSql.PG_db.Insertable(user).ExecuteCommand()==0 ? false:true;
        }

        /// <summary>
        /// 注册查询
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool UserExists(User user)
        {

            return PGSql.PG_db.Queryable<User>().Any(u => u.Name == user.Name);
        }
    }
}
