namespace ToDoApp.Entities;

public class TodoEntity
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }

    public TodoEntity()
    {
        CreatedAt = DateTime.UtcNow;
    }
}