using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.DAL.Repositories.GenericRepository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ApplicationDbContext Context;

    protected GenericRepository(ApplicationDbContext context)
    {
        Context = context;
    }

    public IQueryable<T> GetAll()
    {
        return Context.Set<T>();
    }

    public async Task<T> AddAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        
        await Context.AddAsync(entity);
        
        return entity;
    }

    public Task<T> UpdateAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        Context.Update(entity);

        return Task.FromResult(entity);
    }

    public Task<T> DeleteAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        Context.Remove(entity);

        return Task.FromResult(entity);
    }
}