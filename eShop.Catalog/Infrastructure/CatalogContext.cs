using eShop.Catalog.Domain;
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
            => optionsBuilder.UseSqlServer(@"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=LondonMovingSouthDb;Integrated Security=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatalogItem>()
                .ToTable("CatalogItems");

            modelBuilder.Entity<CatalogItem>()
                .Property(ci => ci.Id)
                .ForSqlServerUseSequenceHiLo("catalog_hilo")
                .IsRequired();

            modelBuilder.Entity<CatalogItem>()
                .Property(ci => ci.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<CatalogItem>()
                .Property(ci => ci.Price)
                .IsRequired();

            modelBuilder.Entity<CatalogItem>()
                .Property(ci => ci.Description)
                .IsRequired();

            modelBuilder.Entity<CatalogItem>()
                .Property(ci => ci.PictureFilename)
                .IsRequired(false);

            modelBuilder.Entity<CatalogItem>()
                .HasOne(ci => ci.CatalogBrand).WithMany()
                .HasForeignKey(ci => ci.CatalogBrandId);

            modelBuilder.Entity<CatalogItem>()
                .HasOne(ci => ci.CatalogType).WithMany()
                .HasForeignKey(ci => ci.CatalogTypeId);

            modelBuilder.Entity<CatalogType>()
                .ToTable("CatalogTypes");

            modelBuilder.Entity<CatalogType>()
                .HasKey(ci => ci.Id);

            modelBuilder.Entity<CatalogType>()
                .Property(ci => ci.Id)
                .ForSqlServerUseSequenceHiLo("catalog_type_hilo")
                .IsRequired();

            modelBuilder.Entity<CatalogType>()
                .Property(ci => ci.Type)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<CatalogBrand>()
                .ToTable("CatalogBrands");


            modelBuilder.Entity<CatalogBrand>()
                .HasKey(ci => ci.Id);

            modelBuilder.Entity<CatalogBrand>()
                .Property(ci => ci.Id)
                .ForSqlServerUseSequenceHiLo("catalog_brand_hilo")
                .IsRequired();

            modelBuilder.Entity<CatalogBrand>()
                .Property(ci => ci.Brand)
                .IsRequired()
                .HasMaxLength(50);
        }
    }

}
