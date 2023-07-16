using Microsoft.EntityFrameworkCore;
using POS_System_1.Models;
using POS_System_1.Views;
using POS_System_1.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace POS_System_1.DatabaseContext
{
    public class POSContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        
        public DbSet<Sale> Sales { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databasePath = @"Database\Datafile1.db";
            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<CartItem>().HasKey(ci => ci.Id);
            modelBuilder.Entity<Sale>().HasKey(s => s.Id);
          

            modelBuilder.Entity<Product>()
    .Property(p => p.SaleId)
    .HasColumnName("SaleId") // Specify the appropriate column name in the database
    .IsRequired(); // Specify the data type of the column

            modelBuilder.Entity<User>()
    .Property(u => u.SaleId)
    .HasColumnName("IsAdmin") // Specify the appropriate column name in the database
    .HasColumnType("int"); // Specify the data type of the column


       

            //  base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Sale>()
        .HasMany(s => s.Products)
        .WithOne(p => p.Sale)
        .HasForeignKey(p => p.SaleId)
        .OnDelete(DeleteBehavior.Cascade);
            // Disable foreign key constraints

          
        }
          

        
     
        internal bool UpdateProduct(int id, Product updatedProduct)
        {
            throw new NotImplementedException();
        }
    }
}
