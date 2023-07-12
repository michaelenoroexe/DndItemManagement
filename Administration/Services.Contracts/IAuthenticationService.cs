using Administration.Shared.DataTransferObjects.DM;
using Administration.Shared.DataTransferObjects.Room;

namespace Administration.Service.Contracts;

public interface IAuthenticationService
{
    Task<bool> ValidateDM(DMForAuthenticationDto dMForAuthentication);
    Task<bool> ValidateRoom(RoomForAuthenticationDto roomInfo);
    string CreateToken(int? dmId, int? characterId);
}
