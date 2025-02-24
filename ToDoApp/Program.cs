using FluentValidation;
using ToDoApp.Mappers;
using ToDoApp.DTOs.todos;
using ToDoApp.Endpoints;
using ToDoApp.Validators.todos;

var builder = WebApplication.CreateBuilder(args);
// registering services 

// registering validation services 
builder.Services.AddTransient<IValidator<CreateTodoDto>, CreateTodoDtoValidator>();
builder.Services.AddTransient<IValidator<UpdateTodoDto>, UpdateTodoDtoValidator>();

// registering service that works with sqlite db and specifying its location
builder.Services.AddSqlite<TodoStoreContext>("Data Source=Data/TodoStore.db");

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();
app.MapTodosEndpoints();

app.Run();