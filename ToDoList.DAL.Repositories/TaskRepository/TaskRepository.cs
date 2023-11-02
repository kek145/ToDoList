using ToDoList.Domain.Entities.DbSet;
using ToDoList.DAL.Configurations.DataContext;
using ToDoList.DAL.Repositories.GenericRepository;

namespace ToDoList.DAL.Repositories.TaskRepository;

public class TaskRepository : GenericRepository<TaskEntity>, ITaskRepository
{
    public TaskRepository(ApplicationDbContext context) : base(context) {}
}