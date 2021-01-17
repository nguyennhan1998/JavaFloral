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

            modelBuilder.Entity<CommentProduct>()
             .HasKey(cp => new { cp.CommentID, cp.ProductID });
            modelBuilder.Entity<CommentProduct>()
                .HasOne(cp => cp.Comment)
                .WithMany(c => c.CommentProducts)
                .HasForeignKey(cp => cp.CommentID);
            modelBuilder.Entity<CommentProduct>()
                .HasOne(cp => cp.Product)
                .WithMany(p => p.CommentProducts)
                .HasForeignKey(cp => cp.ProductID);

            modelBuilder.Entity<CommentAnswer>()
          .HasKey(ca => new { ca.CommentID, ca.AnswerID });
            modelBuilder.Entity<CommentAnswer>()
                .HasOne(ca => ca.Comment)
                .WithMany(c => c.CommentAnswers)
                .HasForeignKey(ca => ca.CommentID);
            modelBuilder.Entity<CommentAnswer>()
                .HasOne(ca => ca.Answer)
                .WithMany(a => a.CommentAnswers)
                .HasForeignKey(ca => ca.AnswerID);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Blog> Blogs {get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentProduct> CommentProducts { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<CommentAnswer> CommentAnswers { get; set; }

    }
}
