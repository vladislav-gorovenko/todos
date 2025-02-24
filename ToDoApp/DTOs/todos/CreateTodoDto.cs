namespace ToDoApp.DTOs.todos;

public record CreateTodoDto(
    string Title,
    string? Description,
    DateTime? DueDate
);