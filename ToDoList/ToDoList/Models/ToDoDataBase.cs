using System;

namespace ToDoList.Models
{
    public class ToDoDataBase
    {
        public int Id { get; set; }
        public string? TaskName { get; set; }
        public string? Description { get; set; }
        public DateOnly? DateOnly { get; set; }
    }
}
