using eShop.Catalog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace eShop.Catalog.Tests.Helpers
{
    public class TestCatalogContext : CatalogContext
    {
        public TestCatalogContext(DbContextOptions<CatalogContext> options, ILogger logger) : base(options, logger)
        {
        }
    }
}
