using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace eShop.Catalog.Infrastructure
{
    public static class WebHostExtensions
    {
        public static IWebHost MigrateDbContext<TContext>(this IWebHost host, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetService<ILogger>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.Information($"Migrating database associated with context {typeof(TContext).Name}");
                    context.Database.Migrate();

                    seeder(context, services);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, $"An error occured while migrating the database used on context {typeof(TContext).Name}");
                }
            }

            return host;
        }
    }
}
