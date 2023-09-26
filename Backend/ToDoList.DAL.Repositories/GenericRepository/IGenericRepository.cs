namespace ToDoList.DAL.Repositories.GenericRepository;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetAll();
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
}