namespace Shared.DataTransferObjects.Room;

public record RoomForUpdateDto : RoomForManipulationDto
{
    public bool Started { get; init; } = false;
}
