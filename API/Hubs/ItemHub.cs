using Entities.Models;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    public class ItemHub : Hub
    {
        public ItemHub() { }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task UpdateItem(Item item) 
        {
            await Clients.Caller.SendAsync("handleMessage", "ServerRespond: " + item.Name);
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
