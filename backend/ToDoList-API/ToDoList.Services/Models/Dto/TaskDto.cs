using System;
using ToDoList.Domain.Enum;

namespace ToDoList.Services.Models.Dto
{
    public class TaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; } = false;
        public Priority Priority { get; set; } = Priority.Easy;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}