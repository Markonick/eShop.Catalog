using System;
using System.Collections.Generic;
using System.Text;
using eShop.Catalog.Domain;
using eShop.Catalog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Moq;
using Serilog;
using Xunit;

namespace eShop.Catalog.Tests
{
    public class CatalogRepositoryTests
    {
        private ICatalogRepository _repository;
        private Mock<ILogger> _logger;

        public CatalogRepositoryTests()
        {
            _logger = new Mock<ILogger>();
            _repository = GetInMemoryCatalogRepository();
        }

        [Fact]
        public void Get_Items_Should_Return_Catalog()
        {

        }

        private ICatalogRepository GetInMemoryCatalogRepository()
        {

            var builder = new DbContextOptionsBuilder<CatalogContext>();
            builder.UseInMemoryDatabase();
            var options = builder.Options;
            var context = new CatalogContext(options, _logger.Object);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return new CatalogRepository(_logger.Object);
        }
    }
}
