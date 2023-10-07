using GroceryWallah.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryWallah.DataAccessLayer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> CartItems { get; set; }
        public DbSet<OrderEFModel> Orders { get; set; }
        public DbSet<OrderDetailsEFModel> OrdersDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {/*
            // Check if there is no admin user
            if (!Users.Any(u => u.IsAdmin))
            {
                // Seed the admin user
                modelBuilder.Entity<User>().HasData(new User
                {
                    UserId = Guid.NewGuid(),
                    FullName = "Admin",
                    Email = "admin@mail.com",
                    Phone = "1234567890",
                    Password = "Adminp@ssword",
                    IsAdmin = true
                });
            }
            */
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
