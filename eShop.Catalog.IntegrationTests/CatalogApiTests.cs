﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using eShop.Catalog.Domain;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace eShop.Catalog.IntegrationTests
{
    public class CatalogApiTests
    {
        private readonly IWebHostBuilder _builder;

        public CatalogApiTests()
        {
            _builder = WebHost.CreateDefaultBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();
        }

        [Fact]
        public async Task Client_should_be_able_to_get_list_of_items()
        {
            using (var server = new TestServer(_builder))
            {
                var response = await server.CreateClient().GetAsync("api/v1/catalog/items");

                response.EnsureSuccessStatusCode();
            }
        }

        [Theory]
        [InlineData(3, 0)]
        [InlineData(3, 1)]
        public async Task Client_should_be_able_to_get_list_of_items_per_page(int pageSize, int pageIndex)
        {
            using (var server = new TestServer(_builder))
            {
                var response = await server.CreateClient().GetAsync($"api/v1/catalog/items?pageSize={pageSize}&pageIndex={pageIndex}");

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<PaginatedItemsViewModel<CatalogItem>>(responseBody);

                Assert.Equal(pageSize, result.PageSize);
                Assert.Equal(pageIndex, result.PageIndex);
            }
        }

        [Fact]
        public async Task Client_should_be_able_to_get_list_of_items_per_name_if_available_and_respond_okstatus()
        {
            using (var server = new TestServer(_builder))
            {
                const string name = "some name asd gndrgh mnjt";
                var response = await server.CreateClient().GetAsync($"api/v1/catalog/items/withname/{name}");

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<PaginatedItemsViewModel<CatalogItem>>(responseBody);

                Assert.Equal(expected: 0, actual: result.Count);
            }
        }

        [Theory]
        [InlineData(1, 4)]
        [InlineData(null, 2)]
        [InlineData(null, null)]
        [InlineData(5, null)]
        public async Task Client_should_be_able_to_get_list_of_items_per_typeid_or_brandid_or_both_and_return_okstatus(int? catalogTypeId, int? catalogBrandId)
        {
            using (var server = new TestServer(_builder))
            {
                var response = await server.CreateClient().GetAsync($"api/v1/catalog/items/type/{catalogTypeId}/brand/{catalogBrandId}");

                response.EnsureSuccessStatusCode();
            }
        }

        [Fact]
        public async Task Client_should_be_able_to_get_list_brands_and_okstatus()
        {
            using (var server = new TestServer(_builder))
            {
                var response = await server.CreateClient().GetAsync("api/v1/catalog/CatalogBrands");

                response.EnsureSuccessStatusCode();
            }
        }

        [Fact]
        public async Task Client_should_be_able_to_get_list_types_and_okstatus()
        {
            using (var server = new TestServer(_builder))
            {
                var response = await server.CreateClient().GetAsync("api/v1/catalog/CatalogTypes");

                response.EnsureSuccessStatusCode();
            }
        }

        [Fact]
        public async Task Client_should_be_able_to_get_item_by_id_and_okstatus()
        {
            using (var server = new TestServer(_builder))
            {
                var response = await server.CreateClient().GetAsync("api/v1/catalog/items/1");

                response.EnsureSuccessStatusCode();
            }
        }

        [Fact]
        public async Task Client_should_return_badrequest_when_id_not_valid()
        {
            using (var server = new TestServer(_builder))
            {
                const int id = 0;
                var response = await server.CreateClient().GetAsync($"api/v1/catalog/items/{id}");

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task Client_should_return_notfound_when_id_not_available()
        {
            using (var server = new TestServer(_builder))
            {
                const int id = 1212121;
                var response = await server.CreateClient().GetAsync($"api/v1/catalog/items/{id}");

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Fact]
        public async Task Client_should_add_new_product_and_return_created_status()
        {
            using (var server = new TestServer(_builder))
            {
                const string uri = "api/v1/catalog/items";
                var item = CreateCatalogItem();

                var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                var response = await server.CreateClient().PostAsync(uri, content);

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }

        [Fact]
        public async Task Client_should_return_500_status_when_item_invalid()
        {
            using (var server = new TestServer(_builder))
            {
                const string uri = "api/v1/catalog/items";

                var content = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json");
                var response = await server.CreateClient().PostAsync(uri, content);

                Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            }
        }

        [Fact]
        public async Task Client_should_return_Created_status_when_item_updated()
        {
            using (var server = new TestServer(_builder))
            {
                const string uri = "api/v1/catalog/items";
                var item = CreateCatalogItem();

                var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                var response = await server.CreateClient().PostAsync(uri, content);
                
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<PaginatedItemsViewModel<CatalogItem>>(responseBody);
                var updatedItem = result.Data.First();

                updatedItem.Price = 1111.00M;

                content = new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json");
                response = await server.CreateClient().PutAsync(uri, content);

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }

        private static CatalogItem CreateCatalogItem()
        {
            var item = new CatalogItem
            {
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
