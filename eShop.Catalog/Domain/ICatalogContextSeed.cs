using System.Threading.Tasks;
using eShop.Catalog.Infrastructure;
using Microsoft.AspNetCore.Hosting;

namespace eShop.Catalog.Domain
{
    public interface ICatalogContextSeed
    {
        Task SeedAsync(CatalogContext context, IHostingEnvironment env);
    }
}