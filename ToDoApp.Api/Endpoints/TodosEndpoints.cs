using ToDoApp.DTOs.todos;

namespace ToDoApp.Endpoints;

public static class TodosEndpoints
{
    const string GetTodoEndpointName = "GetTodo";

    private static List<TodoDto> todos = new List<TodoDto>()
    {
        new TodoDto(1, "Title1", "Description1", new DateTime(2025, 01, 01), false, new DateTime()),
        new TodoDto(2, "Title2", "Description2", new DateTime(2025, 01, 01), false, new DateTime()),
        new TodoDto(3, "Title3", "Description3", new DateTime(2025, 01, 01), false, new DateTime())
    };

    public static RouteGroupBuilder MapTodosEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("todos");


        List<TodoDto> GetTodos()
        {
            return todos;
        }

        IResult GetTodo(int id)
        {
            TodoDto? todo = todos.Find(todo => todo.Id == id);
            return todo is null ? Results.NotFound() : Results.Ok(todo);
        }

        IResult CreateTodo(CreateTodoDto newTodo)
        {
            TodoDto todo = new TodoDto(todos.Count + 1, newTodo.Title, newTodo.Description, newTodo.DueDate, false,
                DateTime.UtcNow);
            todos.Add(todo);

            return Results.CreatedAtRoute(GetTodoEndpointName, new { id = todo.Id }, todo);
        }

        IResult UpdateTodo(int id, UpdateTodoDto updateTodo)
        {
            int index = todos.FindIndex(todo => todo.Id == id);
            if (index > -1)
            {
                todos[index] = new TodoDto(id, updateTodo.Title,
                    updateTodo.Description, updateTodo.DueDate,
                    updateTodo.IsCompleted, todos[index].CreatedAt);
            }

            return Results.NoContent();
        }

        IResult DeleteTodo(int id)
        {
            todos.RemoveAll(todo => todo.Id == id);
            return Results.NoContent();
        }
        
        group.MapGet("/", GetTodos);
        group.MapGet("/{id}", GetTodo).WithName(GetTodoEndpointName);
        group.MapPost("/", CreateTodo);
        group.MapPut("/{id}", UpdateTodo);
        group.MapDelete("/{id}", DeleteTodo);
        
        return group;
    }
}