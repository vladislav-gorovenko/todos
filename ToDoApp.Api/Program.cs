using ToDoApp.DTOs.todos;
using ToDoApp.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapTodosEndpoints();

app.Run();