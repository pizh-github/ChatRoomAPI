using AutoMapper;
using Pizh.NET8.Model;

namespace Pizh.NET8.Extentsion
{
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建映射关系
        /// </summary>
        public CustomProfile()
        {
            CreateMap<Role, RoleVo>()
                .ForMember(a => a.RoleName, o => o.MapFrom(d => d.Name));
            CreateMap<RoleVo, Role>()
                .ForMember(a => a.Name, o => o.MapFrom(d => d.RoleName));

            CreateMap<User, UserVo>()
                .ForMember(a => a.UserName, o => o.MapFrom(d => d.Name));
            CreateMap<UserVo, User>()
                .ForMember(a => a.Name, o => o.MapFrom(d => d.UserName));
        }
    }
}
