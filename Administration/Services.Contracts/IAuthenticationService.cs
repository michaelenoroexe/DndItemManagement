using Administration.Shared.DataTransferObjects.DM;
using Administration.Shared.DataTransferObjects.Room;

namespace Administration.Service.Contracts;

public interface IAuthenticationService
{
    Task<DMDto?> ValidateDM(DMForAuthenticationDto dMForAuthentication);
    Task<RoomDto?> ValidateRoom(RoomForAuthenticationDto roomInfo);
    string CreateToken(int? dmId, int? characterId);
}
