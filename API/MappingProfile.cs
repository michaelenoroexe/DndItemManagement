﻿using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects.Character;
using Shared.DataTransferObjects.CharacterItem;
using Shared.DataTransferObjects.DM;
using Shared.DataTransferObjects.ItemCategory;
using Shared.DataTransferObjects.Room;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DM, DMDto>();
        CreateMap<DMForRegistrationDto, DM>();
        CreateMap<DMForUpdateDto, DM>().ReverseMap();

        CreateMap<Room, RoomDto>();
        CreateMap<RoomForCreationDto, Room>();
        CreateMap<RoomForUpdateDto, Room>().ReverseMap();

        CreateMap<Character, CharacterDto>();
        CreateMap<CharacterForCreationDto, Character>();
        CreateMap<CharacterForUpdateDto, Character>();

        CreateMap<Character, CharacterDto>();
        CreateMap<CharacterForCreationDto, Character>();
        CreateMap<CharacterForUpdateDto, Character>().ReverseMap();

        CreateMap<Character, CharacterDto>();
        CreateMap<CharacterForCreationDto, Character>();
        CreateMap<CharacterForUpdateDto, Character>().ReverseMap();

        CreateMap<ItemCategory, ItemCategoryDto>();
        CreateMap<ItemCategoryForCreationDto, ItemCategory>();
        CreateMap<ItemCategoryForUpdateDto, ItemCategory>().ReverseMap();

        CreateMap<CharacterItem, CharacterItemDto>();
        CreateMap<CharacterItemForCreationDto, CharacterItem>();
        CreateMap<CharacterItemForUpdateDto, CharacterItem>().ReverseMap();
    }
}