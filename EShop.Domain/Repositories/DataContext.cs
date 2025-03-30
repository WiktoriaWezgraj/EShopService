using Microsoft.EntityFrameworkCore;
using EShop.Domain.Models;

namespace EShop.Domain.Repositories
{
    internal class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
