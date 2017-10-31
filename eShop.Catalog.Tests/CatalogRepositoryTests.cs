using System.Linq;
using System.Threading.Tasks;
using eShop.Catalog.Domain;
using eShop.Catalog.Infrastructure;
using eShop.Catalog.Tests.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Moq;
using Serilog;
using Xunit;

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

            var catalogBrands = TestCatalog.CreateBrands();
            var catalogTypes = TestCatalog.CreateTypes();
            var catalogResponse = TestCatalog.CreateItems();

            context.AddRange(catalogBrands);
            context.AddRange(catalogTypes);
            context.AddRange(catalogResponse.ItemsOnPage);
            context.SaveChanges();
        }

        [Fact]
        public async Task Get_Items_Should_Return_Catalog()
        {
            //Arrange
            const int expectedNumberOfItems = 10;

            //Act
            var result = await _repository.GetItemsAsync(0, 10);

            //Assert
            Assert.Equal(result.ItemsOnPage.Count, expectedNumberOfItems);
        }

        [Fact]
        public async Task Get_Items_Should_Return_Catalog_By_Name()
        {
            //Arrange
            const string name = "ba";
            const int expectedNumberOfItems = 2;

            //Act
            var result = await _repository.GetItemsAsync(name, 0, 10);

            //Assert
            Assert.Equal(result.ItemsOnPage.Count, expectedNumberOfItems);
            Assert.Same(result.ItemsOnPage.ElementAt(0).Name, "Gucci Dionysus Small suede and leather shoulder bag");
            Assert.Same(result.ItemsOnPage.ElementAt(1).Name, "Dolce & Gabbana Cotton-blend jacquard blouse");
        }

        [Fact]
        public async Task Get_Items_Should_Return_Catalog_By_CatalogTypeId()
        {
            //Arrange
            const int catalogTypeId = 5;
            const int expectedNumberOfItems = 3;

            //Act
            var result = await _repository.GetItemsAsync(catalogTypeId, null, 0, 14);

            //Assert
            Assert.Equal(result.ItemsOnPage.Count, expectedNumberOfItems);
        }

        [Fact]
        public async Task Get_Items_Should_Return_Catalog_By_CatalogBrandId()
        {
            //Arrange
            const int catalogBrandId = 8;
            const int expectedNumberOfItems = 1;

            //Act
            var result = await _repository.GetItemsAsync(null, catalogBrandId, 0, 14);

            //Assert
            Assert.Equal(result.ItemsOnPage.Count, expectedNumberOfItems);
        }

        [Fact]
        public async Task Get_Brands()
        {
            //Arrange
            const int expectedNumberOfItems = 1;

            //Act
            var result = await _repository.GetCatalogBrandsAsync();

            //Assert
            Assert.Equal(result.Count, expectedNumberOfItems);
        }

        [Fact]
        public async Task Get_Types()
        {
            //Arrange
            const int expectedNumberOfItems = 1;

            //Act
            var result = await _repository.GetCatalogTypesAsync();

            //Assert
            Assert.Equal(result.Count, expectedNumberOfItems);
        }

        private static DbContextOptions<CatalogContext> GetInMemoryContextOptions()
        {
            var builder = new DbContextOptionsBuilder<CatalogContext>();
            builder.UseInMemoryDatabase("TestingDB");
            return builder.Options;
        }
    }
}
