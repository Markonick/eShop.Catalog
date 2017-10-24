using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eShop.Catalog.Domain;
using eShop.Catalog.Helpers;
using Microsoft.AspNetCore.Hosting;
using Polly;
using Serilog;

namespace eShop.Catalog.Infrastructure
{
    public class CatalogContextSeed : ICatalogContextSeed
    {
        private readonly int _retries;
        private readonly int _sleepDurationInSeconds;
        private readonly ILogger _logger;

        public CatalogContextSeed(int retries, int sleepDurationInSeconds, ILogger logger)
        {
            _retries = retries;
            _sleepDurationInSeconds = sleepDurationInSeconds;
            _logger = logger;
        }

        public async Task SeedAsync(CatalogContext context, IHostingEnvironment env)
        {
            const string name = nameof(CatalogContextSeed);

            var retryPolicy = Policy.Handle<SqlException>().WaitAndRetryAsync(
                retryCount: _retries,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(_sleepDurationInSeconds),
                onRetry: (exception, timeSpan, retry, ctx) =>
                {
                    _logger.Debug($"[{name}] Exception {exception.GetType().Name} with message ${exception.Message} detected on attempt {retry} of {_retries}");
                });

            await retryPolicy.ExecuteAsync(async () =>
            {
                var contentRootPath = env.ContentRootPath;

                if (!context.CatalogBrands.Any())
                {
                    var pathToCsv = Path.Combine(contentRootPath, "SeedCSVs", "CatalogBrands.csv");
                    var reader = new FileReader<CatalogBrand>(pathToCsv);
                    await context.CatalogBrands.AddRangeAsync(await reader.GetDataAsync());
                    await context.SaveChangesAsync();
                }
                if (!context.CatalogTypes.Any())
                {
                    var pathToCsv = Path.Combine(contentRootPath, "SeedCSVs", "CatalogTypes.csv");
                    var reader = new FileReader<CatalogType>(pathToCsv);
                    await context.CatalogTypes.AddRangeAsync(await reader.GetDataAsync());
                    await context.SaveChangesAsync();
                }
                if (!context.CatalogItems.Any())
                {
                    var pathToCsv = Path.Combine(contentRootPath, "SeedCSVs", "CatalogItems.csv");
                    var reader = new FileReader<CatalogItem>(pathToCsv);
                    await context.CatalogItems.AddRangeAsync(await reader.GetDataAsync());
                    await context.SaveChangesAsync();
                }

                if (!context.CatalogTypes.Any())
                {
                    await context.CatalogTypes.AddRangeAsync();
                }

                if (!context.CatalogItems.Any())
                {
                    await context.CatalogItems.AddRangeAsync();
                }
            });
        }

        private static IEnumerable<T> GetSeedDataFromFile<T>(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
