using System.Text;
using AutoMapper;
using ToDoList.Domain.Entities.DbSet;
using ToDoList.Domain.Contracts.Request;

namespace ToDoList.Domain.Contracts.MappingProfiles;

public class RequestToDomain : Profile
{
    public RequestToDomain()
    {
        CreateMap<TaskRequest, TaskEntity>()
            .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description,
                opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Priority,
                opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Deadline,
                opt => opt.MapFrom(src => src.Deadline));

        CreateMap<RegistrationRequest, UserEntity>()
            .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email,
                opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PasswordHash,
                opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(src.Password)))
            .ForMember(dest => dest.PasswordSalt,
                opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(src.Password)));
    }
}