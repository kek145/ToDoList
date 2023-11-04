using AutoMapper;
using ToDoList.Domain.Entities.DbSet;
using ToDoList.Domain.Contracts.Request;

namespace ToDoList.Domain.Contracts.MappingProfiles;

public class RequestToDomain : Profile
{
    public RequestToDomain()
    {
        CreateMap<CreateTaskRequestDto, TaskEntity>()
            .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description,
                opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Priority,
                opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Deadline,
                opt => opt.MapFrom(src => src.Deadline));
    }
}