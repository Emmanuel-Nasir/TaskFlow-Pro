namespace TaskFlow.API.Models;

//The class is a blueprint for one task. Basically it defines the shape or each task.
public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set;}
    public string Description { get; set; }
    public DateTime? DueDate { get; set; } //? means that the due date can be null, so it's optional.
    public bool IsCompleted { get; set; } = false;
    public Priority Priority { get; set; } = Priority.Medium;  //By default, the priority is set to Medium when a new task is created.
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

//The enum is a way to define a set of named constants. In this case, it defines the possible values for the priority of a task: Low, Medium, and High.
public enum Priority
{
    Low,
    Medium,
    High
}