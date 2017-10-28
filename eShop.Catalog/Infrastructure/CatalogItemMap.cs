using eShop.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Catalog.Infrastructure
{
    public class CatalogItemMap : IEntityTypeConfiguration<CatalogItem>
    {
        public void Configure(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.ToTable("Catalog");

            builder.Property(ci => ci.Id)
                .ForSqlServerUseSequenceHiLo("catalog_hilo")
                .IsRequired();

            builder.Property(ci => ci.Name)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(ci => ci.Description)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(ci => ci.Price)
                .IsRequired(true);

            builder.Property(ci => ci.PictureFilename)
                .IsRequired(true);

            builder.Property(ci => ci.AvailableStock)
                .IsRequired(true);

            builder.Property(ci => ci.DateTimeAdded)
                .IsRequired(true);

            builder.Property(ci => ci.DateTimeModified)
                .IsRequired(true);

            builder.Property(ci => ci.RestockThreshold)
                .IsRequired(true);

            builder.Property(ci => ci.OnReorder)
                .IsRequired(true);

            builder.Ignore(ci => ci.PictureUri);

            builder.HasOne(ci => ci.CatalogBrand)
                .WithMany()
                .HasForeignKey(ci => ci.CatalogBrandId);

            builder.HasOne(ci => ci.CatalogType)
                .WithMany()
                .HasForeignKey(ci => ci.CatalogTypeId);
        }
    }
}
