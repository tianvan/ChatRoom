using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Server.Hus
{
    public class ChatHub : Hub
    {
        public async Task SendMessageAsync(string message) => await Clients.All.SendAsync("ReceiveMessage", message).ConfigureAwait(false);
    }
}
