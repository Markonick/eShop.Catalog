using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                var response = await server.CreateClient().GetAsync($"api/v1/catalog/items?{pageSize}&{pageIndex}");

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<PaginatedItemsViewModel<CatalogItem>>(responseBody);

                Assert.Equal(pageSize, result.PageSize);
                Assert.Equal(pageIndex, result.PageIndex);
            }
        }
    }
}
