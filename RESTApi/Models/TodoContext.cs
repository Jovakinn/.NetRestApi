namespace RESTApi.Models;

using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions options) : base(options) {}

    public DbSet<TodoItem>? TodoItems { get; set; } = null;
}