using TaskFlow.API.Models;

namespace TaskFlow.API.DTOs;

public class CreateTaskDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public Priority Priority { get; set; } = Priority.Medium;
}