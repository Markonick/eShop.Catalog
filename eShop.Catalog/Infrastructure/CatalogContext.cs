using System;
using eShop.Catalog.Domain;
using eShop.Catalog.Infrastructure.EntityConfigurations;
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
        
        public CatalogContext(DbContextOptions<CatalogContext> options, ILogger logger) : base(options)
        {
            _logger = logger;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.ApplyConfiguration(new CatalogBrandConfiguration());
                modelBuilder.ApplyConfiguration(new CatalogTypeConfiguration());
                modelBuilder.ApplyConfiguration(new CatalogItemConfiguration());
            }
            catch(Exception ex)
            {
                _logger.Debug(ex.Message);
            }
        }
    }
}
