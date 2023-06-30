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

    public ItemHub(IServiceManager services)
    {
        this.services = services;
    }

    public async Task PlayerJoinRoom(int roomId, int characterId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "c" + characterId.ToString());
        await Groups.AddToGroupAsync(Context.ConnectionId, "r" + roomId.ToString());
    }
    public async Task DmJoinRoom(int roomId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "r" + roomId.ToString());
        await ExecForEveryGroup(roomId, (string conn, string group) => Groups.AddToGroupAsync(conn, group));
    }
}
