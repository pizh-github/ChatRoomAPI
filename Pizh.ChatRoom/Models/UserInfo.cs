using SqlSugar;
namespace Pizh.ChatRoom.Models
{
    public class UserInfo
    {
        [SugarColumn(IsPrimaryKey =true)]
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? Password { get; set; }
    }
}
