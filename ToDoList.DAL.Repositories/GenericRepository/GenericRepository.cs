using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ToDoList.DAL.Configurations.DataContext;

namespace ToDoList.DAL.Repositories.GenericRepository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var find = await _context.FindAsync<T>(id);

        if (find == null)
            throw new ArgumentException("An object with the specified ID was not found.", nameof(find));

        return find;
    }

    public async Task<T> AddAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentException("An entity cannot be null.", nameof(entity));

        await _context.AddAsync(entity);

        return entity;
    }

    public Task<T> UpdateAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentException("An entity cannot be null.", nameof(entity));

        _context.Update(entity);

        return Task.FromResult(entity);
    }

    public Task<T> DeleteAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentException("An entity cannot be null.", nameof(entity));

        _context.Remove(entity);

        return Task.FromResult(entity);
    }
}