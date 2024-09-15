using Newtonsoft.Json;
using Pizh.NET8.Model;

namespace Pizh.NET8.Repository.Base
{
    public class BaseRepo<TEntity> : IBaseRepo<TEntity> where TEntity : class
    {
        public BaseRepo()
        {
            PGSql.InitialPGSql("", "", "", "", "");
        }
        public async Task<List<TEntity>> Query()
        {
            await Task.CompletedTask;
            var data = "[{\"Id\":\"01\",\"Name\":\"Pizh\"}]";
            return JsonConvert.DeserializeObject<List<TEntity>>(data) ?? new List<TEntity>();
        }

    }
}
