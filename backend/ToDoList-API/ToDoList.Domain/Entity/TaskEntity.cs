using System;
using ToDoList.Domain.Enum;

namespace ToDoList.Domain.Entity
{
    public class TaskEntity
    {
        public int TaskId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserID{ get; set; }
        public UserEntity? User { get; set; }
    }
}
