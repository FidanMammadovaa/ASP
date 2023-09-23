using ASP_Project.Areas.Identity.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ASP_Project.Areas.Identity.Data.DbContexts;

public class UserContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<PaymentDetail> PaymentDetails { get; set; }

    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Product>().HasKey(p => p.Id);
        builder.Entity<OrderDetail>().HasKey(od => od.Id);
        builder.Entity<Order>().HasKey(o => o.Id);
        builder.Entity<Category>().HasKey(c => c.Id);
        builder.Entity<PaymentDetail>().HasKey(p => p.Id);

        builder.Entity<PaymentDetail>().Property(p => p.Amount).HasColumnType("decimal");
        builder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal");

        base.OnModelCreating(builder);
        
    }
}
