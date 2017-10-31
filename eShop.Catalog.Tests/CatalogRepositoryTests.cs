﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.Catalog.Domain;
using eShop.Catalog.Infrastructure;
using eShop.Catalog.Tests.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using Serilog;
using Xunit;

namespace eShop.Catalog.Tests
{
    public class CatalogRepositoryTests
    {
        private readonly ICatalogRepository _repository;
        private readonly IEnumerable<CatalogBrand> _catalogBrands;
        private readonly IEnumerable<CatalogType> _catalogTypes;
        private readonly CatalogResponse _catalogResponse;
        private readonly CatalogContext _context;
        private IDbContextTransaction _transaction;

        public CatalogRepositoryTests()
        {
            var logger = new Mock<ILogger>();
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            var server = new TestServer(builder);

            _context = server.Host.Services.GetService(typeof(CatalogContext)) as CatalogContext;
            _repository = new CatalogRepository(_context, logger.Object);

            _catalogBrands = TestCatalog.CreateBrands();
            _catalogTypes = TestCatalog.CreateTypes();
            _catalogResponse = TestCatalog.CreateItems();
            _transaction = _context.Database.BeginTransaction();
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
            //Arrange
            const int id = 111;

            //Act
            var result = await _repository.GetItemAsync(id);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Add_item_to_catalog_should_return_true()
        {
            //Arrange
            var item = new CatalogItem
            {
                CatalogBrandId =1,
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
            using (_transaction)
            {
                //Act
                var result = await _repository.AddItemAsync(item);

                //Assert
                Assert.True(result);
            }
        }
    }
}
