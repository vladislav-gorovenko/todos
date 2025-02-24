using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ToDoApp.DTOs.todos;
using ToDoApp.Entities;

namespace ToDoApp.Endpoints;

public static class TodoEndpoints
{
    const string GetTodoEndpointName = "GetTodo";

    public static RouteGroupBuilder MapTodosEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("todos");


        async Task<IResult> GetTodos(TodoStoreContext dbContext, IMapper mapper)
        {
            List<TodoEntity> todoEntities = await dbContext.Todos.AsNoTracking().ToListAsync();
            List<TodoDto> todoDtos = mapper.Map<List<TodoDto>>(todoEntities);
            return Results.Ok(todoDtos);
        }

        async Task<IResult> GetTodo(int id, TodoStoreContext dbContext, IMapper mapper)
        {
            TodoEntity? todo = await dbContext.Todos.FirstOrDefaultAsync(todo => todo.Id == id);
            return todo is null ? Results.NotFound() : Results.Ok(mapper.Map<TodoDto>(todo));
        }
        
        async Task<IResult> CreateTodo(CreateTodoDto newTodo, IValidator<CreateTodoDto> validator, TodoStoreContext dbContext, IMapper mapper)
        {
            var result = await validator.ValidateAsync(newTodo);
            if (!result.IsValid)
            {
                return Results.ValidationProblem(result.ToDictionary());
            }

            TodoEntity todo = mapper.Map<TodoEntity>(newTodo);
            dbContext.Todos.Add(todo);
            await dbContext.SaveChangesAsync();
            
            return Results.CreatedAtRoute(GetTodoEndpointName, new { id = todo.Id }, todo);
        }
        
        async Task<IResult> UpdateTodo(int id, UpdateTodoDto updateTodo, IValidator<UpdateTodoDto> validator, TodoStoreContext dbContext, IMapper mapper)
        {
            TodoEntity? existingTodo = await dbContext.Todos.FindAsync(id);
            if (existingTodo is null)
            {
                return Results.NotFound();
            }
            
            var result = await validator.ValidateAsync(updateTodo);
            if (!result.IsValid)
            {
                return Results.ValidationProblem(result.ToDictionary());
            }

            mapper.Map(updateTodo, existingTodo);
            
            await dbContext.SaveChangesAsync();
        
            return Results.NoContent();
        }
        
        async Task<IResult> DeleteTodo(int id, TodoStoreContext dbContext)
        {
            await dbContext.Todos.Where(todo => todo.Id == id).ExecuteDeleteAsync();
            
            return Results.NoContent();
        }

        group.MapGet("/", GetTodos).Produces<List<TodoDto>>(StatusCodes.Status200OK);
        group.MapGet("/{id}", GetTodo).WithName(GetTodoEndpointName).Produces<TodoDto>(StatusCodes.Status200OK);
        group.MapPost("/", CreateTodo);
        group.MapPut("/{id}", UpdateTodo);
        group.MapDelete("/{id}", DeleteTodo);
        
        return group;
    }
}