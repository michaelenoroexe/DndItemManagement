using AutoMapper;
using Administration.Entities.Models;
using Administration.Shared.DataTransferObjects.DM;
using Administration.Shared.DataTransferObjects.Room;

namespace Administration;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DM, DMDto>();
        CreateMap<DMForRegistrationDto, DM>();
        CreateMap<DMForUpdateDto, DM>().ReverseMap();

        CreateMap<Room, RoomDto>();
        CreateMap<Room, RoomWithDMDto>().ForCtorParam("DMName",
            opt => opt.MapFrom(room => room.DM.Login));
        CreateMap<RoomForCreationDto, Room>();
        CreateMap<RoomForUpdateDto, Room>().ReverseMap();
    }
}
