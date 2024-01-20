using AutoMapper;
using ToDoList.Identity.Domain.Dto;
using ToDoList.Identity.Domain.DbSet;

namespace ToDoList.Identity.Domain.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
    }
}