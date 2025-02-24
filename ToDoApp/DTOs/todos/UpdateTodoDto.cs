namespace ToDoApp.DTOs.todos;

public record UpdateTodoDto(
    string Title,
    string? Description,
    DateTime? DueDate,
    bool IsCompleted
);