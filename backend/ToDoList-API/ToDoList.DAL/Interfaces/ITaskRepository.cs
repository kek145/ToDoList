using System.Threading.Tasks;
using ToDoList.Domain.Entity;
using System.Collections.Generic;

namespace ToDoList.DAL.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskEntity> GetByIdAsync(int taskid);
        Task<List<TaskEntity>> GetAllAsync(int userid);
        Task<TaskEntity> CreateAsync(TaskEntity entity);
        Task<TaskEntity> UpdateAsync(TaskEntity entity);
        Task DeleteAsync(int taskid);
    }
}
