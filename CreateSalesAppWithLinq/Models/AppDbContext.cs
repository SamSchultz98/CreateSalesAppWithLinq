using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLinq.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Orderline> Orderlines { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {



            string connStr = @"server=localhost\sqlexpress;" +
                            "database=SalesDatabase;" + "trusted_connection=true;";




            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(connStr);
            }
        }
            
        
        protected override void OnModelCreating(ModelBuilder builder)           //Will need this for capstone
        {
            

        }
    }
}

