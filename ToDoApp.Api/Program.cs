using FluentValidation;
using ToDoApp.DTOs.todos;
using ToDoApp.Endpoints;
using ToDoApp.Validators.todos;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IValidator<CreateTodoDto>, CreateTodoDtoValidator>();
builder.Services.AddTransient<IValidator<UpdateTodoDto>, UpdateTodoDtoValidator>();
var app = builder.Build();

app.MapTodosEndpoints();

app.Run();