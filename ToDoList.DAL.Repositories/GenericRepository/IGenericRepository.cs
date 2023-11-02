using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoList.DAL.Repositories.GenericRepository;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetById(int id);
    Task<IEnumerable<T>> GetAll();
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
}