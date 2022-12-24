using DataAccess;
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
            Console.WriteLine("Connect");
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
