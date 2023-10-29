using Loginoperations.Model;
using Loginoperations.Model.Order;
using Microsoft.EntityFrameworkCore;



namespace Loginoperations.Context
{
    public class DContext : DbContext
    {
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product?> Products { get; set; }
        public DbSet<User?> Users { get; set; }
        public DbSet<Order?> Orders { get; set; }

        public DContext(DbContextOptions<DContext> options)
            : base(options)
        {
        }
    }
}

