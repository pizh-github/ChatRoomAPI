using AutoMapper;

namespace Pizh.NET8.Extentsion
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(profile: new CustomProfile());
            });
        }
    }
}
