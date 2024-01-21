using System;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Identity.Domain.Dto;
using AutoMapper.QueryableExtensions;
using ToDoList.Identity.Domain.DbSet;
using ToDoList.Identity.Domain.Interfaces;
using ToDoList.Identity.Infrastructure.DataStore;
using UserDto = ToDoList.Identity.Domain.Dto.UserDto;

namespace ToDoList.Identity.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;
    
    public UserRepository(IMapper mapper, ApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public IQueryable<UserDto> GetAll()
    {
        return _context.Users
            .AsQueryable()
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider);
    }

    public async Task<UserDto> AddUserAsync(UserDto userDto, CancellationToken cancellationToken)
    {
        if (userDto is null)
            throw new ArgumentNullException(nameof(userDto), "User is null!");

        var userDtoToUser = _mapper.Map<User>(userDto);

        var user = await _context.Users.AddAsync(userDtoToUser, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var newUser = user.Entity;
        
        var result = _mapper.Map<UserDto>(newUser);
    
        return result;
    }
}