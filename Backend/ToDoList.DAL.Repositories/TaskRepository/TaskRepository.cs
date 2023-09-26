namespace ToDoList.DAL.Repositories.TaskRepository;

public class TaskRepository : GenericRepository<TaskEntity>, ITaskRepository
{
    public TaskRepository(ApplicationDbContext context) : base(context) { }
}