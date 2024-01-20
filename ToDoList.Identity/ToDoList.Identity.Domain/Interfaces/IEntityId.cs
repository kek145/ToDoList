namespace ToDoList.Identity.Domain.Interfaces;

public interface IEntityId<T>
{
    T Id { get; set; }
}