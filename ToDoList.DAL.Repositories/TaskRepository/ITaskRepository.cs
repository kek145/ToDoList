using ToDoList.Domain.Entities.DbSet;
using ToDoList.DAL.Repositories.GenericRepository;

namespace ToDoList.DAL.Repositories.TaskRepository;

public interface ITaskRepository : IGenericRepository<TaskEntity>
{
    
}