using SqlSugar;

namespace Pizh.NET8.Model
{
    /// <summary>
    /// 用户
    /// </summary>
    [SugarTable("user_table")]
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Role { get; set; }

        public string? Password { get; set; }

    }
}
