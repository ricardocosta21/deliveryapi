using System;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using Supermarket.API.Domain.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Supermarket.API.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        //public DbSet<Stripe> Stripes { get; set; }

        public IConfiguration Configuration { get; }

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {           
            optionsBuilder.UseMySQL(Configuration["ConnectionStrings:DefaultConnection"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}