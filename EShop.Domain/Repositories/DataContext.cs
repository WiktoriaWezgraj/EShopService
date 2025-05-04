using EShop.Domain.Models;
using EShop.Domain.Seeders;
using Microsoft.EntityFrameworkCore;

namespace EShop.Domain.Repositories;

public class DataContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasData(EShopSeeder.GetInitialCategories());
        modelBuilder.Entity<Product>().HasData(EShopSeeder.GetInitialProducts());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseInMemoryDatabase("ProductsDb");
    }
}
