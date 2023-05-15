using Shared.DataTransferObjects.DM;
using Shared.DataTransferObjects.Room;

namespace Service.Contracts;

public interface IAuthenticationService
{
    Task<bool> ValidateDM(DMForAuthenticationDto dMForAuthentication);
    Task<bool> ValidateRoom(RoomForAuthenticationDto roomInfo);
    string CreateToken(string? dmLogin, int? roomId);
}
