using Microsoft.EntityFrameworkCore;
using ECommerceSystem.Models;

namespace ECommerce_System
{
    internal class ECommerceContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        // Bridge table between Order and Product
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<Review> Reviews { get; set; }
    }
}