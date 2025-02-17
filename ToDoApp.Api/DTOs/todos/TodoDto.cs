namespace ToDoApp.DTOs.todos;

public record TodoDto(
    int Id,
    string Title,
    string? Description,
    DateTime? DueDate,
    bool IsCompleted,
    DateTime CreatedAt
);