using System;
using System.Threading;
using System.Threading.Tasks;

using ChatRoom.Server.Services;

using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Server.Hubs
{
    public class ChatHub : Hub
    {
        private readonly INicknameGenerator _nicknameGenerator;

        public ChatHub(INicknameGenerator nicknameGenerator)
        {
            _nicknameGenerator = nicknameGenerator;
        }

        private static int s_onlineUsers;

        public async Task SendMessageAsync(string message, DateTime sendedTime) =>
            await Clients.All.SendAsync("MessageReceived", message, sendedTime, _nicknameGenerator.Generate(Context.ConnectionId).ToString(), Context.ConnectionId).ConfigureAwait(false);

        public override async Task OnConnectedAsync()
        {
            Interlocked.Increment(ref s_onlineUsers);
            await Clients.All.SendAsync("OnlineUserJoined", s_onlineUsers, _nicknameGenerator.Generate(Context.ConnectionId)).ConfigureAwait(false);
            await base.OnConnectedAsync().ConfigureAwait(false);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Interlocked.Decrement(ref s_onlineUsers);
            await Clients.All.SendAsync("OnlineUserLeaved", s_onlineUsers, _nicknameGenerator.Generate(Context.ConnectionId)).ConfigureAwait(false);
            _nicknameGenerator.Remove(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception).ConfigureAwait(false);
        }
    }
}
