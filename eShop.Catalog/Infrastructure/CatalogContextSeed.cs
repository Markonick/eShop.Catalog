using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eShop.Catalog.Domain;
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

                try
                {
                    if (!context.CatalogBrands.Any())
                    {
                        var csvFileCatalogBrands = Path.Combine(contentRootPath, "SeedFiles", "CatalogBrands.csv");
                        var reader = new CsvFileReader<CatalogBrand>(csvFileCatalogBrands);
                        await context.CatalogBrands.AddRangeAsync(await reader.GetDataAsync());
                        await context.SaveChangesAsync();

                    }

                    if (!context.CatalogTypes.Any())
                    {
                        var csvFileCatalogTypes = Path.Combine(contentRootPath, "SeedFiles", "CatalogTypes.csv");
                        var reader = new CsvFileReader<CatalogType>(csvFileCatalogTypes);
                        await context.CatalogTypes.AddRangeAsync(await reader.GetDataAsync());
                        await context.SaveChangesAsync();
                    }

                    if (!context.CatalogItems.Any())
                    {
                        var csvFileCatalogItems = Path.Combine(contentRootPath, "SeedFiles", "CatalogItems.csv");
                        var reader = new CsvFileReader<CatalogItem>(csvFileCatalogItems);
                        await context.CatalogItems.AddRangeAsync(await reader.GetDataAsync());
                        await context.SaveChangesAsync();
                    }
                }
                catch(Exception ex)
                {
                    _logger.Debug(ex.Message);
                }
            });
        }
    }
}
