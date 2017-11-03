using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using eShop.Catalog.API;
using eShop.Catalog.Domain;
using eShop.Catalog.UnitTests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Serilog;
using Xunit;

namespace eShop.Catalog.UnitTests
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
            _controller = new CatalogController(_repository.Object, _logger.Object);
        }

        [Fact]
        public async Task Items_Should_Return_HttpOk()
        {
            //Arrange
            var items = TestCatalog.CreateItems();
            _repository.Setup(x => x.GetItemsAsync(0, 10)).Returns(Task.FromResult(items));

            //Act
            var actionResult = await _controller.Items(10, 0) as ObjectResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task Items_By_Name_Should_Return_HttpOk()
        {
            //Arrange
            const string name = "ba";
            var items = TestCatalog.CreateItems();
            _repository.Setup(x => x.GetItemsAsync(name, 0, 10)).Returns(Task.FromResult(items));

            //Act
            var actionResult = await _controller.Items(name, 10, 0) as ObjectResult;
            
            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task Items_By_Brand_And_TypeId_Should_Return_HttpOk()
        {
            //Arrange
            var items = TestCatalog.CreateItems();
            _repository.Setup(x => x.GetItemsAsync(1, 1, 0, 10)).Returns(Task.FromResult(items));

            //Act
            var actionResult = await _controller.Items(1, 1, 10, 0) as ObjectResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task Get_Item_By_Id_Should_Return_HttpOk()
        {
            //Arrange
            var item = new CatalogItem {AvailableStock = 1, Name = "name"};
            var id = 1;
            _repository.Setup(x => x.GetItemAsync(id)).Returns(Task.FromResult(item));

            //Act
            var actionResult = await _controller.GetById(id) as ObjectResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_Item_By_Id_Should_Return_HttpNotFound_when_no_items_available()
        {
            //Arrange
            _repository.Setup(x => x.GetItemAsync(1)).Returns(Task.FromResult((CatalogItem) null));

            //Act
            var actionResult = await _controller.GetById(1) as NotFoundResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_Item_By_Id_Should_Return_HttpBadRequest_when_id_0()
        {
            //Arrange
            var item = new CatalogItem { AvailableStock = 1, Name = "name" };
            var id = 1;
            _repository.Setup(x => x.GetItemAsync(id)).Returns(Task.FromResult(item));

            //Act
            var actionResult = await _controller.GetById(0) as BadRequestResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_Catalog_Types_Should_Return_HttpOk()
        {
            //Arrange
            _repository.Setup(x => x.GetCatalogTypesAsync()).Returns(Task.FromResult(new List<CatalogType>()));

            //Act
            var actionResult = await _controller.CatalogTypes() as ObjectResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_Catalog_Brands_Should_Return_HttpOk()
        {
            //Arrange
            var item = new CatalogItem { AvailableStock = 1, Name = "name" };
            _repository.Setup(x => x.GetCatalogBrandsAsync()).Returns(Task.FromResult(new List<CatalogBrand>()));

            //Act
            var actionResult = await _controller.CatalogBrands() as ObjectResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_Product_Should_Return_Created()
        {
            //Arrange
            var item = new CatalogItem { Id = 666, AvailableStock = 1, Name = "name" };
            _repository.Setup(x => x.AddItemAsync(item)).Returns(Task.FromResult(item));

            //Act
            var actionResult = await _controller.CreateProduct(item) as ObjectResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.Created);
        }

        [Fact]
        public async Task Create_Product_Should_Return_BadRequest_with_invalid_product()
        {
            //Arrange
            _repository.Setup(x => x.AddItemAsync(null)).Returns(Task.FromResult((CatalogItem)null));

            //Act
            var actionResult = await _controller.CreateProduct(null) as BadRequestResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.BadRequest);
        }
        
        [Fact]
        public async Task Delete_Product_Should_Return_NoContent()
        {
            //Arrange
            var item = new CatalogItem { Id = 1, AvailableStock = 1, Name = "name" };
            _repository.Setup(x => x.DeleteItemAsync(item.Id)).Returns(Task.FromResult(item));

            //Act
            var actionResult = await _controller.DeleteProduct(item.Id) as NoContentResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_Product_Should_Return_NotFound_With_Non_Existing_Product()
        {
            //Arrange
            var item = new CatalogItem { Id = 1, AvailableStock = 1, Name = "name" };
            _repository.Setup(x => x.DeleteItemAsync(item.Id)).Returns(Task.FromResult((CatalogItem)null));

            //Act
            var actionResult = await _controller.DeleteProduct(item.Id) as NotFoundResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Update_Product_Should_Return_Created()
        {
            //Arrange
            var item = new CatalogItem { AvailableStock = 1, Name = "name" };
            _repository.Setup(x => x.UpdateItemAsync(item)).Returns(Task.FromResult(item));

            //Act
            var actionResult = await _controller.UpdateProduct(item) as ObjectResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.Created);
        }

        [Fact]
        public async Task Update_Product_Should_Return_NotFound_With_Non_Existing_Product()
        {
            //Arrange
            var item = new CatalogItem { AvailableStock = 1, Name = "name" };
            _repository.Setup(x => x.UpdateItemAsync(item)).Returns(Task.FromResult((CatalogItem)null));

            //Act
            var actionResult = await _controller.UpdateProduct(item) as NotFoundResult;

            //Assert
            Assert.NotNull(actionResult);
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.NotFound);
        }
    }
}
