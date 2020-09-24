using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Server.Hus
{
    public class ChatHub : Hub
    {
        private static int s_onlineUsers;

        public async Task SendMessageAsync(string message, DateTime sendedTime)
        {
            var userIp = Context.Features.Get<IHttpConnectionFeature>().RemoteIpAddress.ToString();
            await Clients.All.SendAsync("MessageReceived", message, sendedTime, userIp).ConfigureAwait(false);
        }

        public override async Task OnConnectedAsync()
        {
            Interlocked.Increment(ref s_onlineUsers);
            await Clients.All.SendAsync("OnlineUsersChanged", s_onlineUsers).ConfigureAwait(false);
            await base.OnConnectedAsync().ConfigureAwait(false);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Interlocked.Decrement(ref s_onlineUsers);
            await Clients.All.SendAsync("OnlineUsersChanged", s_onlineUsers).ConfigureAwait(false);
            await base.OnDisconnectedAsync(exception).ConfigureAwait(false);
        }
    }
}
