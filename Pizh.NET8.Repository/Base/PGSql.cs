using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizh.NET8.Repository.Base
{
    /// <summary>
    /// 数据库访问基类
    /// </summary>
    public class PGSql
    {
        public static SqlSugarScope PG_db = null;
        /// <summary>
        /// 初始化数据库连接
        /// </summary>
        /// <param name="server">主机IP</param>
        /// <param name="port">端口</param>
        /// <param name="database">数据库名称</param>
        /// <param name="userId">用户名</param>
        /// <param name="passWord">密码</param>
        public static void InitialPGSql(string server, string port, string database, string userId, string passWord)
        {
            PG_db = new SqlSugarScope(new ConnectionConfig
            {
                ConnectionString = $"Server={server};Port={port};Database={database};User Id={userId};Password={passWord}",
                DbType = DbType.PostgreSQL,
                IsAutoCloseConnection = true,
            });  // 使用SqlSugarScope建立PostgreSQL数据库连接


        }
        /// <summary>
        /// 获取数据库Postgre当前时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetPostgreDateTime()
        {
            return PG_db.Ado.GetDateTime("select now();");
        }


    }
}
