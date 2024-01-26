using AutoMapper;
using ToDoList.Domain.DbSet;
using ToDoList.Domain.Dto;

namespace ToDoList.Application.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
    }
}