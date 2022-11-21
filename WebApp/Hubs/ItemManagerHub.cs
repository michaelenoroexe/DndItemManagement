using DataAccess;
using Microsoft.AspNetCore.SignalR;

namespace WebApp.Hubs
{
    public class ItemManagerHub : Hub
    {
        public async Task SendItemAsync() => 
            await Clients.All.SendAsync("itemChanged", new[] { new Item(Guid.NewGuid(), "Тестовый предмет", 1337)});

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
