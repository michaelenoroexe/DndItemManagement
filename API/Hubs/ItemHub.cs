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

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
