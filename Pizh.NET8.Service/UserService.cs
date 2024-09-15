using AutoMapper;
using Pizh.NET8.IService;
using Pizh.NET8.Model;
using Pizh.NET8.Repository;
using Pizh.NET8.Repository.Base;

namespace Pizh.NET8.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public PGSql? Context { get; }

        public User Login(string username, string password)
        {
            return _userRepo.Login(username, password);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="userVo"></param>
        /// <returns></returns>
        public bool Register(UserVo userVo)
        {
            User user = _mapper.Map<User>(userVo);
            if (!_userRepo.UserExists(user))
            {
                return _userRepo.Register(user);
            }
            else
            {
                throw new Exception("User already exists!");
            }
        }
    }
}
