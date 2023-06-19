using System;
using ToDoList.Domain.Enum;

namespace ToDoList.Models.Dto
{
    public class TaskDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required bool Status { get; set; } = false;
        public required Priority Priority { get; set; } = Priority.Easy;
        public required DateTime Created { get; set; } = DateTime.UtcNow;
        public required int UserID { get; set; }
    }
}
