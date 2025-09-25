using Microsoft.AspNetCore.SignalR;

namespace PrivateChat.Connection
{
    class ChatHub : Hub
    {

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

    }
}