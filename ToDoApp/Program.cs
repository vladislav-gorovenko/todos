using FluentValidation;
using ToDoApp.Mappers;
using ToDoApp.DTOs.todos;
using ToDoApp.Endpoints;
using ToDoApp.Validators.todos;

var builder = WebApplication.CreateBuilder(args);

// Register Validation Services
builder.Services.AddTransient<IValidator<CreateTodoDto>, CreateTodoDtoValidator>();
builder.Services.AddTransient<IValidator<UpdateTodoDto>, UpdateTodoDtoValidator>();

// Register SQLite DbContext
var connectionString = builder.Configuration.GetConnectionString("TodoStore");
builder.Services.AddSqlite<TodoStoreContext>(connectionString);

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