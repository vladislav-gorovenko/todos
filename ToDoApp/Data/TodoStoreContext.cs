using Microsoft.EntityFrameworkCore;
using ToDoApp.Entities;

public class TodoStoreContext : DbContext
{
    public TodoStoreContext(DbContextOptions<TodoStoreContext> options) : base(options)
    {}

    public DbSet<TodoEntity> Todos => Set<TodoEntity>();
}