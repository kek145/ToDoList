namespace ToDoList.Domain.Contracts.MappingProfiles;

public class DomainToResponse : Profile
{
    public DomainToResponse()
    {
        CreateMap<UserEntity, GetUserResponse>()
            .ForMember(dest => dest.UserId,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.Email,
                opt => opt.MapFrom(src => src.Email));

        CreateMap<UserEntity, GetUserFullNameResponse>()
            .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        
        CreateMap<UserEntity, GetUserInfoResponse>()
            .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.Email,
                opt => opt.MapFrom(src => src.Email));

        CreateMap<TaskEntity, GetTaskResponse>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description,
                opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Priority,
                opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Deadline,
                opt => opt.MapFrom(src => src.Deadline));
    }
}