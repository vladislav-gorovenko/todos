var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/todos", () => "Hello World!");

app.Run();