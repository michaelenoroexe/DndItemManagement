namespace Shared.DataTransferObjects.Room; 

public record RoomWithDMDto(int Id, string Name, string DMName) : RoomDto(Id, Name);
