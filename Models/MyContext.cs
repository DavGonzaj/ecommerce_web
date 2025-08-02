using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Add this
using Microsoft.AspNetCore.Identity;

namespace ecommerce_web.Models;

public class MyContext : IdentityDbContext<IdentityUser>
{
    public MyContext() { }
    public MyContext(DbContextOptions options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
}
