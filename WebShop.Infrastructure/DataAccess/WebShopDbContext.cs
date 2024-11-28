
using Microsoft.EntityFrameworkCore;
using WebShop.Domain.Models;

namespace WebShop.Infrastructure.DataAccess
{
    public class WebShopDbContext : DbContext
    {
        public WebShopDbContext(DbContextOptions<WebShopDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderProducts { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<OrderDetail>()
            //    .HasKey(op => new { op.OrderId, op.ProductId });

            // OrderDetail: Order (Many-to-One)
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.Id);

            // OrderDetail: Product (Many-to-One)
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
