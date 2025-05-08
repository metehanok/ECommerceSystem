using ECommerceSystem.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Data.Repositories
{
    public class ECommerceDbContext:DbContext
    {
        public ECommerceDbContext()
        {
                
        }
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext>options):base(options)
        {}
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet <Products> Products { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=ECommerceSystemManagementServer;Trusted_Connection=True;",
                    options=>options.MigrationsAssembly("ECommerceSystem.Data")
                    );
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Orders>()
                .HasOne(o=>o.Customers)
                .WithMany(c=>c.Orders)
                .HasForeignKey(b=>b.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderDetails>()
                .HasOne(od=>od.Products)
                .WithMany(o=>o.OrderDetails)
                .HasForeignKey(f=>f.ProductId) 
                .OnDelete(DeleteBehavior.Restrict);//ilişkili verinin silinmesine kısıtlama getirir
            
            modelBuilder.Entity<Products>()
                .HasOne(p=>p.Category)
                .WithMany(p=>p.Products)
                .HasForeignKey(f=>f.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);//ilişkili veri silindiğinde product-categoryıd null olacak

            modelBuilder.Entity<OrderDetails>()
                .HasOne(o=>o.Orders)
                .WithMany(od=>od.OrderDetails)
                .HasForeignKey(f=>f.OrderId)
                .OnDelete(DeleteBehavior.Cascade);//sipariş silinirse detayları da silinie
            modelBuilder.Entity<Products>()
                 .Property(p => p.Price)
                 .HasPrecision(18, 2);

            modelBuilder.Entity<OrderDetails>()
                .Property(o => o.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderDetails>()
                .Property(o => o.TotalPrice)
                .HasPrecision(18, 2);
        }
    }
}
