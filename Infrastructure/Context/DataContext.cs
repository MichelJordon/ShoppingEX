using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) 
    { 
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        
    }
    public DbSet<Credit> Credits { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products {get; set;}

}