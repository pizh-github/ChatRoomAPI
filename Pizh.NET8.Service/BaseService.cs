using AutoMapper;
using Pizh.NET8.IService;
using Pizh.NET8.Model;
using Pizh.NET8.Repository;
using Pizh.NET8.Repository.Base;

namespace Pizh.NET8.Service
{
    public class BaseService<TEntity, TVo> : IBseService<TEntity, TVo> where TEntity : class
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepo<TEntity> _baseRepo;

        /// <summary>
        /// 注入AutoMapper
        /// </summary>
        /// <returns></returns>
        public BaseService(IMapper mapper, IBaseRepo<TEntity> baseRepo)
        {
            _mapper = mapper;
            _baseRepo = baseRepo;
        }
        public async Task<List<TVo>> Query()
        {
            //var baseRepo = new BaseRepo<TEntity>();
            //var entites =  await baseRepo.Query();
            var entites = await _baseRepo.Query(); 

            var res = _mapper.Map<List<TVo>>(entites);
            return res;
        }

    }
}
