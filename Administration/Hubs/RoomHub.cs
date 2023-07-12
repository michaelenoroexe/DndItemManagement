using Microsoft.AspNetCore.SignalR;
using Administration.Service.Contracts;

namespace Administration.Hubs;
public class RoomHub : Hub
{
    private readonly IRoomService roomService;

    public RoomHub(IRoomService roomService)
    {
        this.roomService = roomService;
    }
    public override async Task OnConnectedAsync()
    {
        var rooms = await roomService.GetAllRooms(false);
        await Clients.Caller.SendAsync("AddedRooms", rooms );
        await base.OnConnectedAsync();
    }
}
