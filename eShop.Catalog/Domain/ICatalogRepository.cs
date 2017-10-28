using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShop.Catalog.Domain
{
    public interface ICatalogRepository
    {
        Task<CatalogResponse> GetItemsAsync(int pageIndex, int pageSize);
        Task<CatalogResponse> GetItemsAsync(string name, int pageIndex, int pageSize);
        Task<CatalogResponse> GetItemsAsync(int? catalogTypeId, int? catalogBrandId, int pageIndex, int pageSize);
        Task<CatalogItem> GetItemAsync(int id);
        Task<bool> AddItemAsync(CatalogItem product);
        Task<bool> DeleteItemAsync(int id);
        Task<bool> UpdateItemAsync(CatalogItem product);
        Task<List<CatalogBrand>> GetCatalogBrandsAsync();
        Task<List<CatalogType>> GetCatalogTypesAsync();
    }
}