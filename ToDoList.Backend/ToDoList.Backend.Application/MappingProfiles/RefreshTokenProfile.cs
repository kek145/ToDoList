using AutoMapper;
using ToDoList.Domain.DbSet;
using ToDoList.Domain.Dto;

namespace ToDoList.Application.MappingProfiles;

public class RefreshTokenProfile : Profile
{
    public RefreshTokenProfile()
    {
        CreateMap<RefreshToken, RefreshTokenDto>();
        CreateMap<RefreshTokenDto, RefreshToken>();
    }
}