using ToDoList.DAL.Core.DataContext;
using ToDoList.Domain.Entities.DbSet;
using ToDoList.DAL.Contracts.Interfaces;

namespace ToDoList.DAL.Repositories.Repositories;

public class TaskRepository : GenericRepository<TaskEntity>, ITaskRepository
{
    public TaskRepository(ApplicationDbContext context) : base(context) { }
}