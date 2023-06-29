using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Service.Contracts;
using Shared.DataTransferObjects.CharacterItem;
using Shared.DataTransferObjects.Item;
using Shared.DataTransferObjects.Room;
using SignalRSwaggerGen.Attributes;
using System.Security.Claims;

namespace API.Hubs;
[SignalRHub]
public class ItemHub : Hub
{
    private readonly IServiceManager services;
    private readonly IMapper mapper;
    private readonly IHubContext<RoomHub> roomContext;

    private async Task ChangeStateOfRoom(int dmId, Room room, bool newState)
    {
        room.Started = newState;
        var roomforUpdate = mapper.Map<RoomForUpdateDto>(room);

        await services.RoomService.UpdateRoomAsync(dmId, room.Id, roomforUpdate, false, true);
    }

    private async Task ActivateRoom(string dmLogin, int roomId)
    {
        var dm = await services.DMService.GetDMAsync(dmLogin, false);
        var room = await services.RoomService.GetFullRoomAsync(roomId, false);
        if (room.DmId != dm.Id) throw new UnauthorizedAccessException();

        await ChangeStateOfRoom(dm.Id, room, true);

        await ExecForEveryGroup(room.Id, (string conn, string group) => Groups.AddToGroupAsync(conn, group));
        await Groups.AddToGroupAsync(Context.ConnectionId, "r" + roomId.ToString());

        var roomDto = mapper.Map<RoomDto>(room);
        await roomContext.Clients.All.SendAsync("ChangeRoom", roomDto);
    }
    private async Task DeactivateRoom(string dmLogin)
    {
        var dm = await services.DMService.GetDMAsync(dmLogin, false);
        var roomDto = services.RoomService.GetRoomsForDM(dm.Id, false).Result.FirstOrDefault(r => r.Started);
        if (roomDto is null) return;
        var room = mapper.Map<Room>(roomDto);

        await ChangeStateOfRoom(dm.Id, room, false);

        await ExecForEveryGroup(room.Id, (string conn, string group) => Groups.RemoveFromGroupAsync(conn, group));
        await roomContext.Clients.All.SendAsync("ChangeRoom", roomDto);
    }
    private async Task ExecForEveryGroup(int roomId, Func<string, string, Task> exec)
    {
        var groups = await GetAllCharacters(roomId);
        var tasks = new List<Task>();
        foreach (string group in groups)
        {
            tasks.Add(exec(Context.ConnectionId, group));
        }
        await Task.WhenAll(tasks);
    }
    private async Task<IEnumerable<string>> GetAllCharacters(int roomId)
    {
        var characters = await services.CharacterService.GetRoomCharacters(roomId, false);

        return characters.Select(c => "c" + c.Id.ToString());
    }

    public ItemHub(IServiceManager services, IMapper mapper, IHubContext<RoomHub> roomContext)
    {
        this.services = services;
        this.mapper = mapper;
        this.roomContext = roomContext;
    }

    public async Task ActivateRoom(int roomId)
    {
        var dmLogin = Context.User?.FindFirst(ClaimTypes.Name)?.Value;
        if (dmLogin is null) throw new UnauthorizedAccessException();

        await ActivateRoom(dmLogin, roomId);
    }
    public async Task JoinRoom(RoomForAuthenticationDto? room)
    {
        var chId = Context.User?.FindFirst(ClaimTypes.Actor)?.Value;
        if (room is null && chId is null) throw new ArgumentNullException();
        if (room is not null)
        {
            var valResult = await services.AuthenticationService.ValidateRoom(room);
            if (valResult == false) throw new UnauthorizedAccessException();
            var dmLogin = Context.User?.FindFirst(ClaimTypes.Name)?.Value;
            string token = services.AuthenticationService.CreateToken(dmLogin, room.CharacterId);
            await Clients.Caller.SendAsync("GetToken", token);
            await Groups.AddToGroupAsync(Context.ConnectionId, "c" + room.CharacterId!.Value.ToString());
            await Groups.AddToGroupAsync(Context.ConnectionId, "r" + room.Id!.Value.ToString());
            return;
        }
        if (chId is not null)
        {
            var ch = await services.CharacterService.GetCharacterAsync(Convert.ToInt32(chId), false);
            await Groups.AddToGroupAsync(Context.ConnectionId, "c" + ch.Id.ToString());
            await Groups.AddToGroupAsync(Context.ConnectionId, "r" + ch.RoomId.ToString());
            return;
        }
    }
    public async override Task OnDisconnectedAsync(Exception? exception)
    {
        var dmLogin = Context.User?.FindFirst(ClaimTypes.Name)?.Value;
        if (dmLogin is not null) await DeactivateRoom(dmLogin);
        await base.OnDisconnectedAsync(exception);    
    }
}
