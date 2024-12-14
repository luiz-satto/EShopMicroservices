using Discount.GRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Data
{
    public class DiscountContext(DbContextOptions<DiscountContext> options) : DbContext(options)
    {
        public DbSet<Coupon> Coupons { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon() { Id = 1, ProductName = "iPhone X", Description = "iPhone X - 10% Discount!", Amount = 10 },
                new Coupon() { Id = 2, ProductName = "Samsung 10", Description = "Samsung 10 - 20% Discount!", Amount = 10 }
            );
        }
    }
}
