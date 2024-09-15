using Pizh.NET8.Model;

namespace Pizh.NET8.IService
{
    public interface IBseService<TEntity, TVo> where TEntity : class
    {
        Task<List<TVo>> Query();

    }
}
