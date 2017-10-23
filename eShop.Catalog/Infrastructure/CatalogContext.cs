﻿using eShop.Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace eShop.Catalog.Infrastructure
{
    public class CatalogContext : DbContext
    {
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }

        public CatalogContext() { }
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options) { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(@"Data Source=.\sqlexpress;Initial Catalog=LondonMovingSouthDb;Integrated Security=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatalogItem>().ToTable("CatalogItems");
            modelBuilder.Entity<CatalogType>().ToTable("CatalogTypes");
            modelBuilder.Entity<CatalogBrand>().ToTable("CatalogBrands");
        }
    }

}
