using Microsoft.EntityFrameworkCore;
using Sandwish.Server.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sandwish.Server.Repository
{
    public class SandwishDbContext : DbContext
    {
        public SandwishDbContext(DbContextOptions<SandwishDbContext> options)
           : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ItemCart> ItemCarts {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(b => b.Ingredients)
                .WithOne();
        }
    }
}
