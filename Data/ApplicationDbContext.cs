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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<JavaFloral.ViewModels.ProductViewModel> ProductViewModel { get; set; }
        public DbSet<JavaFloral.Models.Blog> Blog { get; set; }
    }
}
