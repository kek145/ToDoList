using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Entities.Enums;

public enum Priority
{
    [Display(Name = "Easy")]
    Easy = 1,
    [Display(Name = "Hard")]
    Medium = 2,
    [Display(Name = "Medium")]
    Hard = 3
}