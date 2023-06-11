using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.SignalR;
using Service.Contracts;
using Shared.DataTransferObjects.DM;
using Shared.DataTransferObjects.Room;
using SignalRSwaggerGen.Attributes;
using System.Security.Claims;

namespace API.Hubs;
[SignalRHub]
public class RoomHub : Hub
{
    private readonly IServiceManager services;

    public RoomHub(IServiceManager service)
    {
        this.services = service;
    }
    public override async Task OnConnectedAsync()
    {
        var rooms = await services.RoomService.GetAllRooms(false);
        await Clients.Caller.SendAsync("AddedRoom", rooms );
        await base.OnConnectedAsync();
    }
}
