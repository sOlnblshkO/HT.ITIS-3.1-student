using Dotnet.Homeworks.MainProject.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Homeworks.MainProject.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    
    public AppDbContext() { }

    public AppDbContext(DbContextOptions options) : base(options) { }
}