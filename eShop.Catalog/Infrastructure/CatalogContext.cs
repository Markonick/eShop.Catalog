using System;
using eShop.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace eShop.Catalog.Infrastructure
{
    public class CatalogContext : DbContext
    {
        private readonly ILogger _logger;
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }

        public CatalogContext() { }

        public CatalogContext(DbContextOptions<CatalogContext> options, ILogger logger) : base(options)
        {
            _logger = logger;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(@"Server=(LocalDb)\MSSQLLocalDB;Initial Catalog=LondonMovingSouthDb;Integrated Security=True");
            //=> optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=LondonMovingSouthDb;Integrated Security=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.ApplyConfiguration(new CatalogBrandMap());
                modelBuilder.ApplyConfiguration(new CatalogTypeMap());
                modelBuilder.ApplyConfiguration(new CatalogItemMap());
            }
            catch(Exception ex)
            {
                _logger.Debug(ex.Message);
            }
        }
    }
}
