using DataAccess;
using Microsoft.AspNetCore.SignalR;

namespace WebApp.Hubs
{
    public class ItemManagerHub : Hub
    {
        public ItemManagerHub()
        {
            // TODO: Create scheme to handle requests.
        }

        public async Task ItemChanged(Item item) => 
            await Clients.All.SendAsync("itemChanged", item);

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
