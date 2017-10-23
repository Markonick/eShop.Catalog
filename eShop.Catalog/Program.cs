using System.IO;
using eShop.Catalog.Domain;
using eShop.Catalog.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eShop.Catalog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .MigrateDbContext<CatalogContext>((context, services) =>
                {
                    var env = services.GetService<IHostingEnvironment>();
                    var seeder = services.GetService<ICatalogContextSeed>();
                    seeder.SeedAsync(context, env);
                })
                .Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    config.AddEnvironmentVariables();
                })
                .Build();
    }
}
