using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShop.Catalog.Domain
{
    public interface ICatalogRepository
    {
        Task<bool> AddProductAsync(CatalogItem product);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> UpdateProductAsync(CatalogItem product);
        Task<CatalogItem> GetProductAsync(int id);
        Task<List<CatalogItem>> GetCatalogAsync(string count, string offset, DateTime? fromDate, DateTime? toDate);
    }
}