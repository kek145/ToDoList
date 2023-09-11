using ToDoList.Domain.Entities.DbSet;

namespace ToDoList.DAL.Contracts.Interfaces;

public interface ITaskRepository : IGenericRepository<TaskEntity> { }