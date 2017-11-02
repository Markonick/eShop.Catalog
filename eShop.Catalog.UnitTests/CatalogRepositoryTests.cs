using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.Catalog.Domain;
using eShop.Catalog.Infrastructure;
using eShop.Catalog.UnitTests.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Moq;
using Serilog;
using Xunit;

namespace eShop.Catalog.UnitTests
{
    public class CatalogRepositoryTests
    {
        private readonly ICatalogRepository _repository;
        private readonly CatalogContext _context;
        private readonly IEnumerable<CatalogBrand> _catalogBrands;
        private readonly IEnumerable<CatalogType> _catalogTypes;
        private readonly CatalogResponse _catalogResponse;

        public CatalogRepositoryTests()
        {
            var logger = new Mock<ILogger>();

            var builder = new WebHostBuilder()
                            .UseEnvironment("UnitTesting")
                            .UseStartup<Startup>();

            var server = new TestServer(builder);

            _context = server.Host.Services.GetService(typeof(CatalogContext)) as CatalogContext;
            
            _repository = new CatalogRepository(_context, logger.Object);
            
            _catalogBrands = TestCatalog.CreateBrands();
            _catalogTypes = TestCatalog.CreateTypes();
            _catalogResponse = TestCatalog.CreateItems();
            
            _context.AddRange(_catalogBrands);
            _context.AddRange(_catalogTypes);
            _context.AddRange(_catalogResponse.ItemsOnPage);
            _context.SaveChanges();
            
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
            Assert.Same(result.ItemsOnPage.ElementAt(0).Name, "Dolce & Gabbana Cotton-blend jacquard blouse");
            Assert.Same(result.ItemsOnPage.ElementAt(1).Name, "Gucci Dionysus Small suede and leather shoulder bag");
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
        public async Task Get_Items_Should_Return_Catalog_When_Given_Null_Ids()
        {
            //Arrange
            const int expectedNumberOfItems = 14;

            //Act
            var result = await _repository.GetItemsAsync(null, null, 0, 14);

            //Assert
            Assert.Equal(result.ItemsOnPage.Count, expectedNumberOfItems);
        }

        [Fact]
        public async Task Get_Items_Should_Throw_Exception_when_context_disposed()
        {
            //Arrange
            //Act
            _context.Dispose();

            //Assert
            await Assert.ThrowsAsync<ObjectDisposedException>(() => _repository.GetItemsAsync(null, null, 0, 14));
        }

        [Fact]
        public async Task Get_Brands()
        {
            //Arrange
            //Act
            var result = await _repository.GetCatalogBrandsAsync();

            //Assert
            Assert.Equal(result.Count, _catalogBrands.Count());
        }

        [Fact]
        public async Task Get_Types()
        {
            //Arrange
            //Act
            var result = await _repository.GetCatalogTypesAsync();

            //Assert
            Assert.Equal(result.Count, _catalogTypes.Count());
        }

        [Fact]
        public async Task Get_item_by_id_should_return_item_if_id_exists()
        {
            //Arrange
            const int id = 1;

            //Act
            var result = await _repository.GetItemAsync(id);

            //Assert
            Assert.NotNull(result);
            Assert.Same(result.Name, "Gucci Dionysus Small suede and leather shoulder bag");
        }

        [Fact]
        public async Task Get_item_by_id_should_return_item_if_id_doesnt_exist()
        {
            const int id = 111;

            //Act
            var result = await _repository.GetItemAsync(id);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Add_item_to_catalog_should_return_added_item()
        {
            //Arrange
            var item = CreateCatalogItem();

            //Act
            var result = await _repository.AddItemAsync(item);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Id > 0, "The newly added Id is not larger than 0!");
        }

        [Fact]
        public async Task Add_invalid_item_to_catalog_should_throw()
        {
            //Arrange
            CatalogItem item = null;

            //Act
            //Assert
            await Assert.ThrowsAnyAsync<Exception>(() => _repository.AddItemAsync(item));
        }

        [Fact]
        public async Task Deleting_an_item_should_return_deleted_item()
        {
            //Arrange
            var item = CreateCatalogItem();

            //Act
            var addedItem = await _repository.AddItemAsync(item);
            var deletedItem = await _repository.DeleteItemAsync(addedItem.Id);

            //Assert
            Assert.NotNull(deletedItem);
            Assert.True(deletedItem.Id > 0, "The newly added Id is not larger than 0!");
        }

        [Fact]
        public async Task Deleting_a_non_existing_item_should_return_null()
        {
            //Arrange
            //Act
            var deletedItem = await _repository.DeleteItemAsync(1111);

            //Assert
            Assert.Null(deletedItem);
        }

        [Fact]
        public async Task Updating_an_item_should_return_update_item()
        {
            //Arrange
            var item = CreateCatalogItem();

            //Act
            var addedItem = await _repository.AddItemAsync(item);
            var updatedItem = addedItem;
            updatedItem.Price = 1000.00M;

            var result = await _repository.UpdateItemAsync(updatedItem);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Id > 0, "The newly added Id is not larger than 0!");
        }

        private static CatalogItem CreateCatalogItem()
        {
            var item = new CatalogItem
            {
                Id = 666,
                CatalogBrandId = 1,
                CatalogTypeId = 1,
                AvailableStock = 1,
                DateTimeAdded = DateTime.Now,
                DateTimeModified = DateTime.Now,
                Description = "blabla",
                Name = "somename",
                OnReorder = false,
                PictureFilename = "picture.png",
                Price = 100.00M,
                RestockThreshold = 10
            };
            return item;
        }
    }
}
