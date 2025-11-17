using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    public class CategoryDbContext : DbContext
    {
        // DbSet for Category table
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Using SQLite for easy setup (no SQL Server required)
            // Database file will be created as "category.db" in the project directory
            optionsBuilder.UseSqlite("Data Source=category.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the self-referencing relationship
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.ChildCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Create indexes for better query performance
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.ParentCategoryId);

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name);
        }
    }
}
