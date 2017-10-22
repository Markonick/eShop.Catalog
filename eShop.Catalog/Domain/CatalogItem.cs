using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace eShop.Catalog.Domain
{
    public class CatalogItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureFilename { get; set; }
        public string PictureUri { get; set; }
        public int CatalogTypeId { get; set; }
        public CatalogType CatalogType { get; set; }
        public int CatalogBrandId { get; set; }
        public CatalogBrand CatalogBrand { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public DateTime DateTimeModified { get; set; }
        public int AvailableStock { get; set; }
        public int RestockThreshold { get; set; }
        public bool OnReorder { get; set; }
        public int RemoveStock(int quantity)
        {
            if (AvailableStock == 0)
            {
                throw new Exception($"Out of Stock, product item {Name} is sold out");
            }

            var removed = Math.Min(quantity, AvailableStock);

            AvailableStock -= removed;

            if (AvailableStock == 0)
            {
                OnReorder = true;
            }

            return removed;
        }

        public int AddStock(int quantity)
        {
            var original = AvailableStock;

            if (AvailableStock + quantity > RestockThreshold)
            {
                AvailableStock += RestockThreshold - AvailableStock;
            }
            else
            {
                AvailableStock += quantity;
            }

            OnReorder = false;

            return AvailableStock - original;
        }
    }
}
