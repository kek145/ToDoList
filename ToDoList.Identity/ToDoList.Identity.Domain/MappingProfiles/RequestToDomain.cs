using System.Text;
using AutoMapper;
using ToDoList.Identity.Domain.Dto;
using ToDoList.Identity.Domain.Requests;

namespace ToDoList.Identity.Domain.MappingProfiles;

public class RequestToDomain : Profile
{
    public RequestToDomain()
    {
        CreateMap<RegistrationRequest, UserDto>()
            .ForMember(dest => dest.Email,
                opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PasswordHash,
                opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(src.Password)))
            .ForMember(dest => dest.PasswordSalt,
                opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(src.Password)));
    }
}