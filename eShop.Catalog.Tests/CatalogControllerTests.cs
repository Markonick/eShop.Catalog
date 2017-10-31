using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using eShop.Catalog.API;
using eShop.Catalog.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Serilog;
using Xunit;

namespace eShop.Catalog.Tests
{

    public class CatalogControllerTests
    {
        private readonly Mock<ICatalogRepository> _repository;
        private readonly Mock<ILogger> _logger;
        private readonly CatalogController _controller;

        public CatalogControllerTests()
        {
            _logger = new Mock<ILogger>();
            _repository = new Mock<ICatalogRepository>();
        }

        [Fact]
        public async Task CatalogControllerItems_Should_Return_HttpOk()
        {
            //Arrange
            var items = CreateCatalog();
            _repository.Setup(x => x.GetItemsAsync(0, 10)).Returns(Task.FromResult(items));

            //Act
            var controller = new CatalogController(_repository.Object, _logger.Object);
            var actionResult = await controller.Items(10, 0) as ObjectResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task CatalogControllerItems_By_Name_Should_Return_HttpOk()
        {
            //Arrange
            const string name = "ba";
            var items = CreateCatalog();
            _repository.Setup(x => x.GetItemsAsync(name, 0, 10)).Returns(Task.FromResult(items));

            //Act
            var controller = new CatalogController(_repository.Object, _logger.Object);
            var actionResult = await controller.Items(name, 10, 0) as ObjectResult;
            
            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task CatalogControllerItems_By_Brand_And_TypeId_Should_Return_HttpOk()
        {
            //Arrange
            var items = CreateCatalog();
            _repository.Setup(x => x.GetItemsAsync(1, 1, 0, 10)).Returns(Task.FromResult(items));

            //Act
            var controller = new CatalogController(_repository.Object, _logger.Object);
            var actionResult = await controller.Items(1, 1, 10, 0) as ObjectResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task CatalogController_GetById_Should_Return_HttpOk()
        {
            //Arrange
            var item = new CatalogItem {AvailableStock = 1, Name = "name"};
            var id = 1;
            _repository.Setup(x => x.GetItemAsync(id)).Returns(Task.FromResult(item));

            //Act
            var controller = new CatalogController(_repository.Object, _logger.Object);
            var actionResult = await controller.GetById(id) as ObjectResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task CatalogController_GetById_Should_Return_HttpNotFound_when_no_items_available()
        {
            //Arrange
            _repository.Setup(x => x.GetItemAsync(1)).Returns(Task.FromResult((CatalogItem) null));

            //Act
            var controller = new CatalogController(_repository.Object, _logger.Object);
            var actionResult = await controller.GetById(1) as NotFoundResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CatalogController_GetById_Should_Return_HttpBadRequest_when_id_0()
        {
            //Arrange
            var item = new CatalogItem { AvailableStock = 1, Name = "name" };
            var id = 1;
            _repository.Setup(x => x.GetItemAsync(id)).Returns(Task.FromResult(item));

            //Act
            var controller = new CatalogController(_repository.Object, _logger.Object);
            var actionResult = await controller.GetById(0) as BadRequestResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CatalogController_GetCatalogTypes_Should_Return_HttpOk()
        {
            //Arrange
            _repository.Setup(x => x.GetCatalogTypesAsync()).Returns(Task.FromResult(new List<CatalogType>()));

            //Act
            var controller = new CatalogController(_repository.Object, _logger.Object);
            var actionResult = await controller.CatalogTypes() as ObjectResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task CatalogController_GetCatalogBrands_Should_Return_HttpOk()
        {
            //Arrange
            var item = new CatalogItem { AvailableStock = 1, Name = "name" };
            var id = 1;
            _repository.Setup(x => x.GetCatalogBrandsAsync()).Returns(Task.FromResult(new List<CatalogBrand>()));

            //Act
            var controller = new CatalogController(_repository.Object, _logger.Object);
            var actionResult = await controller.CatalogBrands() as ObjectResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task CatalogController_CreateProduct_Should_Return_Created()
        {
            //Arrange
            var item = new CatalogItem { AvailableStock = 1, Name = "name" };
            _repository.Setup(x => x.AddItemAsync(item)).Returns(Task.FromResult(true));

            //Act
            var controller = new CatalogController(_repository.Object, _logger.Object);
            var actionResult = await controller.CreateProduct(item) as ObjectResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.Created);
        }

        [Fact]
        public async Task CatalogController_CreateProduct_Should_Return_BadRequest_with_invalid_product()
        {
            //Arrange
            _repository.Setup(x => x.AddItemAsync(null)).Returns(Task.FromResult(false));

            //Act
            var controller = new CatalogController(_repository.Object, _logger.Object);
            var actionResult = await controller.CreateProduct(null) as BadRequestResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.BadRequest);
        }
        
        [Fact]
        public async Task CatalogController_DeleteProduct_Should_Return_NoContent()
        {
            //Arrange
            var item = new CatalogItem { Id = 1, AvailableStock = 1, Name = "name" };
            _repository.Setup(x => x.DeleteItemAsync(item.Id)).Returns(Task.FromResult(true));

            //Act
            var controller = new CatalogController(_repository.Object, _logger.Object);
            var actionResult = await controller.DeleteProduct(item.Id) as NoContentResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task CatalogController_DeleteProduct_Should_Return_NotFound()
        {
            //Arrange
            var item = new CatalogItem { Id = 1, AvailableStock = 1, Name = "name" };
            _repository.Setup(x => x.DeleteItemAsync(item.Id)).Returns(Task.FromResult(false));

            //Act
            var controller = new CatalogController(_repository.Object, _logger.Object);
            var actionResult = await controller.DeleteProduct(item.Id) as NotFoundResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CatalogController_UpdateProduct_Should_Return_Created()
        {
            //Arrange
            var item = new CatalogItem { AvailableStock = 1, Name = "name" };
            _repository.Setup(x => x.UpdateItemAsync(item)).Returns(Task.FromResult(true));

            //Act
            var controller = new CatalogController(_repository.Object, _logger.Object);
            var actionResult = await controller.UpdateProduct(item) as ObjectResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.Created);
        }

        [Fact]
        public async Task CatalogController_UpdateProduct_Should_Return_NotFound()
        {
            //Arrange
            var item = new CatalogItem { AvailableStock = 1, Name = "name" };
            _repository.Setup(x => x.UpdateItemAsync(item)).Returns(Task.FromResult(false));

            //Act
            var controller = new CatalogController(_repository.Object, _logger.Object);
            var actionResult = await controller.UpdateProduct(item) as NotFoundResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.NotFound);
        }

        private CatalogResponse CreateCatalog()
        {
            List<CatalogItem> items = new List<CatalogItem>
            {
                new CatalogItem{ Name = "Gucci Dionysus Small suede and leather shoulder bag", Description = new String('a', 50),
                    Price = 1710.00M, PictureFilename = "Gucci_bag_1.png", CatalogTypeId = 2,
                    CatalogBrandId = 1, AvailableStock = 10, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Balenciaga Classic City Textured-leather Tote - Black - one size", Description = new String('b', 50),
                    Price = 1045.00M, PictureFilename = "Balenciaga_bag_1.png", CatalogTypeId = 2,
                    CatalogBrandId = 3, AvailableStock = 5, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Prada Pumps & High Heels", Description = new String('c', 50),
                    Price = 287.00M, PictureFilename = "Prada_shoes_1.png", CatalogTypeId = 1,
                    CatalogBrandId = 2, AvailableStock = 7, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Marc Jacobs Lucille Platform Pumps", Description = new String('d', 50),
                    Price = 288.00M, PictureFilename = "MJacobs_shoes_1.png", CatalogTypeId = 1,
                    CatalogBrandId = 4, AvailableStock = 3, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Pierre Cardin Acrylic Scarf Purple Women", Description = new String('e', 50),
                    Price = 30.00M, PictureFilename = "PCardin_accessory_1.png", CatalogTypeId = 3,
                    CatalogBrandId = 5, AvailableStock = 32, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = true },

                new CatalogItem{ Name = "J.Crew Womens Ruffly Tulle Dress", Description = new String('f', 50),
                    Price = 98.00M, PictureFilename = "JCrew_dress_1.png", CatalogTypeId = 6,
                    CatalogBrandId = 6, AvailableStock = 12, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Dolce & Gabbana Cotton-blend jacquard blouse", Description = new String('g', 50),
                    Price = 575.00M, PictureFilename = "DnB_top_1.png", CatalogTypeId = 5,
                    CatalogBrandId = 7, AvailableStock = 25, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "AllSaints Mina Top", Description = new String('h', 50),
                    Price = 65.00M, PictureFilename = "AllSaints_top_1.png", CatalogTypeId = 5,
                    CatalogBrandId = 8, AvailableStock = 25, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "J Brand Low Rise Ankle Cropped Skinny Jeans - Estate", Description = new String('i', 50),
                    Price = 95.00M, PictureFilename = "JBrand_jeans_1.png", CatalogTypeId = 7,
                    CatalogBrandId = 9, AvailableStock = 45, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = true },

                new CatalogItem{ Name = "Cheap Monday Women's Dig Arched Logo Black T-Shirt", Description = new String('j', 50),
                    Price = 25.00M, PictureFilename = "CMonday_tee_1.png", CatalogTypeId = 9,
                    CatalogBrandId = 10, AvailableStock = 50, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Vintage Champion Reverse Weave small logo pullover", Description = new String('k', 50),
                    Price = 27.50M, PictureFilename = "Vintage_Champion_top_1.png", CatalogTypeId = 4,
                    CatalogBrandId = 35, AvailableStock = 30, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Fendi Fur-embellished Cotton-Blend Sweatshirt", Description = new String('l', 50),
                    Price = 522.00M, PictureFilename = "Fendi_top_1.png", CatalogTypeId = 5,
                    CatalogBrandId = 11, AvailableStock = 11, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Burberry Flared Stretch Jeans-Size: 29-Black", Description = new String('m', 50),
                    Price = 295.00M, PictureFilename = "Burberry_jeans_1.png", CatalogTypeId = 7,
                    CatalogBrandId = 12, AvailableStock = 7, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Balenciaga Low Top Logo Sneakers - White - IT 37", Description = new String('n', 50),
                    Price = 435.00M, PictureFilename = "Balenciaga_shoes_1.png", CatalogTypeId = 1,
                    CatalogBrandId = 3, AvailableStock = 21, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false }
            };

            return new CatalogResponse {ItemsOnPage = items, TotalItems = items.Count};
        }
    }
}
