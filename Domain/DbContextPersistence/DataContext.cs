using Domain.Models;
using Domain.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Models.DbModels.Dishes;

namespace Domain.DbContextPersistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<Dishes> Dishes { get; set; }
        public DbSet<DishIngredient> DishIngredient { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dishes>()
                .HasMany(b => b.Ingredients)
                .WithOne();
        }
    }
}

    