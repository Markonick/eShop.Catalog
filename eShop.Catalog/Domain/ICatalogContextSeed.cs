using System.Threading.Tasks;
using eShop.Catalog.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace eShop.Catalog.Domain
{
    public interface ICatalogContextSeed
    {
        Task SeedAsync(CatalogContext context, IHostingEnvironment env, ILogger logger);
    }
}