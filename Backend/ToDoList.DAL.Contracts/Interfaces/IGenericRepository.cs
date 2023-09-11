using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.DAL.Contracts.Interfaces;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetAll();

    Task<T> CreateAsync(T entity);

    Task<T> UpdateAsync(T entity);

    Task<T> RemoveAsync(T entity);
}