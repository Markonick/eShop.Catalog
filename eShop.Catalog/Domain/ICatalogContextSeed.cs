using System.Threading.Tasks;

namespace eShop.Catalog.Domain
{
    public interface ICatalogContextSeed
    {
        Task SeedAsync();
    }
}