using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JavaFloral.Models;
using JavaFloral.ViewModels;

namespace JavaFloral.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderProducts>()
                .HasKey(op => new { op.OrderID, op.ProductID });
            modelBuilder.Entity<OrderProducts>()
                .HasOne(op => op.Orders)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderID);
            modelBuilder.Entity<OrderProducts>()
                .HasOne(op => op.Products)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op => op.ProductID);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Blog> Blogs {get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }

    }
}
