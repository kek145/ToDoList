using System;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.DAL.Core.DataContext;
using ToDoList.DAL.Contracts.Interfaces;

namespace ToDoList.DAL.Repositories.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> GetAll()
    {
        return _context.Set<T>();
    }

    public async Task<T> CreateAsync(T entity)
    {
        if(entity == null)
            throw new ArgumentNullException("Entity is null");

        await _context.AddAsync(entity);

        return entity;
    }

    public Task<T> UpdateAsync(T entity)
    {
        if(entity == null)
            throw new ArgumentNullException("Entity is null");

        _context.Update(entity);

        return Task.FromResult(entity);
    }

    public Task<T> RemoveAsync(T entity)
    {
        if(entity == null)
            throw new ArgumentNullException("Entity is null");

        _context.Remove(entity);

        return Task.FromResult(entity);
    }
}