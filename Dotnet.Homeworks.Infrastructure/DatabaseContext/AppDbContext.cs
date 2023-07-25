using Dotnet.Homeworks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Homeworks.Infrastructure.DatabaseContext; 

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<Product> Products { get; set; }
    
    public AppDbContext() { }

    public AppDbContext(DbContextOptions options) : base(options) { }
}