using AutoMapper;
using ToDoList.Identity.Domain.Dto;
using ToDoList.Identity.Domain.Responses;

namespace ToDoList.Identity.Domain.MappingProfiles;

public class DomainToResponse : Profile
{
    public DomainToResponse()
    {
        CreateMap<UserDto, UserResponse>()
            .ForMember(dest => dest.UserId,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Email,
                opt => opt.MapFrom(src => src.Email));
    }
}