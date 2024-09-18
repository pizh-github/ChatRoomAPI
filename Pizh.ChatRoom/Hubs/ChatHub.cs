using Microsoft.AspNetCore.SignalR;

namespace Pizh.ChatRoom.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string username, string message)
        {
            DateTime time = DateTime.Now;
            await Clients.All.SendAsync("ReceiveMessage", username, message, time.ToString());
        }
    }
}
