namespace Administration.Shared.DataTransferObjects.Room; 

public record RoomWithDMDto(int Id, string Name, bool Started, string DMName) : RoomDto(Id, Name, Started);
