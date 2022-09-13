using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MyProject.Entities;

namespace MyProject.Data;

public class AppDbContext : DbContext
{
  public DbSet<User>? Users { get; set; }
  public AppDbContext(DbContextOptions options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }
}