using SqlSugar;

namespace Pizh.ChatRoom.Models
{
    public class UserMessage
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
