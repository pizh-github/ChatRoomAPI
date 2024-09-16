using AutoMapper.Configuration;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Pizh.NET8.Model;
using SqlSugar;
using System.Configuration;

namespace Pizh.NET8.Repository.Base
{
    public class BaseRepo<TEntity> : IBaseRepo<TEntity> where TEntity : class
    {
        public BaseRepo(Configuration configuration)
        {
            string server = configuration.GetConfigValue<string>("postgre:server");
            string port = configuration.GetConfigValue<string>("postgre:port");
            string database = configuration.GetConfigValue<string>("postgre:database");
            string userid = configuration.GetConfigValue<string>("postgre:userid");
            string password = configuration.GetConfigValue<string>("postgre:password");
            PGSql.InitialPGSql(server, port, database, userid, password);
        }
        public async Task<List<TEntity>> Query()
        {
            await Task.CompletedTask;
            var data = "[{\"Id\":\"01\",\"Name\":\"Pizh\"}]";
            return JsonConvert.DeserializeObject<List<TEntity>>(data) ?? new List<TEntity>();
        }

    }
}
