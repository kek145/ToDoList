using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ToDoList.DAL.Configurations.DataContext;

namespace ToDoList.DAL.Repositories.GenericRepository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ApplicationDbContext Context;

    public GenericRepository(ApplicationDbContext context)
    {
        Context = context;
    }

    public async Task<T> GetById(int id)
    {
        var find = await Context.FindAsync<T>(id);

        if (find == null)
            throw new NullReferenceException("Not found!");

        return find;
    }

    public virtual async Task<IEnumerable<T>> GetAll()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        if (entity == null)
            throw new NullReferenceException(nameof(entity));

        await Context.AddAsync(entity);

        return entity;
    }

    public Task<T> UpdateAsync(T entity)
    {
        if (entity == null)
            throw new NullReferenceException(nameof(entity));

        Context.Update(entity);

        return Task.FromResult(entity);
    }

    public Task<T> DeleteAsync(T entity)
    {
        if (entity == null)
            throw new NullReferenceException(nameof(entity));

        Context.Remove(entity);

        return Task.FromResult(entity);
    }
}