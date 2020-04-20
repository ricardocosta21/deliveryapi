using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using Supermarket.API.Domain.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Supermarket.API.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public IConfiguration Configuration { get; }

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Database needs to point to mysqlserver container
            //optionsBuilder.UseSqlServer("Server=localhost;Database=master;user id=sa;password=Passw0rd");
            //optionsBuilder.UseSqlServer("Server=localhost;Database=master;user id=sa;password=Passw0rd");
            //optionsBuilder.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]);
            optionsBuilder.UseMySQL(Configuration["ConnectionStrings:DefaultConnection"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}