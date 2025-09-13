using Microsoft.AspNetCore.SignalR;

namespace PrivateChat.Connection
{
    class ChatHub : Hub
    {
        
        //TODO: add dictionary or that thing later on, i forgor what it is for fuck sake
        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {

            await base.OnDisconnectedAsync(exception);
        }
    }
}