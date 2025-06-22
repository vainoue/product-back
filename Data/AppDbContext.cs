using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;
using ProductAPI.Data;

namespace ProductAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }
    
    public DbSet<User> Users { get; set; }
}