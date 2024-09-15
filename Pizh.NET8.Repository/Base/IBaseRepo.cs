using Pizh.NET8.Model;
using SqlSugar;

namespace Pizh.NET8.Repository.Base
{
    public interface IBaseRepo<Tentity> where Tentity : class
    {
        Task<List<Tentity>> Query();
    }
}
