using FluentValidation;
using ToDoApp.Mappers;
using ToDoApp.DTOs.todos;
using ToDoApp.Endpoints;
using ToDoApp.Validators.todos;

var builder = WebApplication.CreateBuilder(args);

// Register validation services
builder.Services.AddTransient<IValidator<CreateTodoDto>, CreateTodoDtoValidator>();
builder.Services.AddTransient<IValidator<UpdateTodoDto>, UpdateTodoDtoValidator>();

// Register SQLite DbContext
builder.Services.AddSqlite<TodoStoreContext>("Data Source=Data/TodoStore.db");

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Register Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapTodosEndpoints();

app.Run();