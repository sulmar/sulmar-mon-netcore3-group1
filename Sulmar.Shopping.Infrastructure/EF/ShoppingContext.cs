using Microsoft.EntityFrameworkCore;
using Sulmar.Shopping.Domain;
using Sulmar.Shopping.Infrastructure.EF.Configurations;

namespace Sulmar.Shopping.Infrastructure.EF
{
    public class ShoppingContext : DbContext
    {
        public ShoppingContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new CustomerConfiguration())
                .ApplyConfiguration(new ProductConfiguration());

            // TODO: custom conventions



            base.OnModelCreating(modelBuilder);
        }
    }
}
