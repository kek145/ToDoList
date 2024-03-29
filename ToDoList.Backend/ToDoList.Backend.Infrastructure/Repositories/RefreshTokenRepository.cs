﻿using System.Linq;
using AutoMapper;
using System.Threading;
using ToDoList.Domain.Dto;
using ToDoList.Domain.DbSet;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using ToDoList.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using ToDoList.Infrastructure.DataStore;

namespace ToDoList.Infrastructure.Repositories;

public class RefreshTokenRepository(IMapper mapper, ApplicationDbContext context) : IRefreshTokenRepository
{
    private readonly IMapper _mapper = mapper;
    private readonly ApplicationDbContext _context = context;

    public IQueryable<RefreshTokenDto> GetAll()
    {
        return _context.RefreshTokens
            .AsQueryable()
            .ProjectTo<RefreshTokenDto>(_mapper.ConfigurationProvider);
    }

    public async Task<int> DeleteRefreshTokenAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await _context.RefreshTokens
            .Where(x => x.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<RefreshTokenDto> AddRefreshTokenAsync(RefreshTokenDto refreshTokenDto, CancellationToken cancellationToken = default)
    {
        var refreshToken = _mapper.Map<RefreshToken>(refreshTokenDto);

        var newToken = await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<RefreshTokenDto>(newToken.Entity);

        return result;
    }

    public async Task<int> UpdateRefreshTokenAsync(int tokenId, string refreshToken, CancellationToken cancellationToken = default)
    {
        return await _context.RefreshTokens
            .Where(x => x.Id == tokenId)
            .ExecuteUpdateAsync(x => 
                x.SetProperty(y => y.Token, refreshToken), cancellationToken);
    }
}