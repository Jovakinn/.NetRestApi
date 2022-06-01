using Microsoft.EntityFrameworkCore;
using RESTApi.Models;

namespace RESTApi.Context;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions options) : base(options) {}
    public DbSet<TodoItem>? TodoItems { get; set; } = null;
}