using EShop.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace User.Domain.Repositories
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //relacja wiele-do-wielu między User a Role
        }
    }
}

