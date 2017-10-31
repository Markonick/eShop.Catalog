using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using eShop.Catalog.Domain;
using eShop.Catalog.Infrastructure;
using eShop.Catalog.Tests.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Moq;
using Serilog;
using Xunit;
using Xunit.Sdk;

namespace eShop.Catalog.Tests
{
    public class CatalogRepositoryTests
    {
        private readonly ICatalogRepository _repository;

        public CatalogRepositoryTests()
        {
            var logger = new Mock<ILogger>();
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            var server = new TestServer(builder);

            var context = server.Host.Services.GetService(typeof(CatalogContext)) as CatalogContext;
            _repository = new CatalogRepository(context, logger.Object);

            var catalogResponse = TestCatalog.Create();

            foreach (var item in catalogResponse.ItemsOnPage)
            {
                context.CatalogItems.Add(item);
            }

            context.SaveChanges();
        }

        [Fact]
        public async Task Get_Items_Should_Return_Catalog()
        {

            var result = await _repository.GetItemsAsync(0, 10);

            var expectedNumberOfItems = 10;
            Assert.Equal(result.TotalItems, expectedNumberOfItems);
        }

        private static DbContextOptions<CatalogContext> GetInMemoryContextOptions()
        {
            var builder = new DbContextOptionsBuilder<CatalogContext>();
            builder.UseInMemoryDatabase("TestingDB");
            return builder.Options;
        }
    }
}
