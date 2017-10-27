using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using eShop.Catalog.Domain;
using Microsoft.AspNetCore.Hosting;
using Polly;
using Serilog;

namespace eShop.Catalog.Infrastructure
{
    public class CatalogContextSeed : ICatalogContextSeed
    {
        private readonly int _retries;
        private readonly int _sleepDurationInSeconds;
        private readonly ILogger _logger;

        public CatalogContextSeed(int retries, int sleepDurationInSeconds, ILogger logger)
        {
            _retries = retries;
            _sleepDurationInSeconds = sleepDurationInSeconds;
            _logger = logger;
        }

        public async Task SeedAsync(CatalogContext context, IHostingEnvironment env)
        {
            const string name = nameof(CatalogContextSeed);

            var retryPolicy = Policy.Handle<SqlException>().WaitAndRetryAsync(
                retryCount: _retries,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(_sleepDurationInSeconds),
                onRetry: (exception, timeSpan, retry, ctx) =>
                {
                    _logger.Debug($"[{name}] Exception {exception.GetType().Name} with message ${exception.Message} detected on attempt {retry} of {_retries}");
                });

            await retryPolicy.ExecuteAsync(async () =>
            {
                try
                {
                    if (!context.CatalogBrands.Any())
                    {
                        await context.CatalogBrands.AddRangeAsync(CatalogBrandsInit());
                        await context.SaveChangesAsync();

                    }

                    if (!context.CatalogTypes.Any())
                    {
                        await context.CatalogTypes.AddRangeAsync(CatalogTypesInit());
                        await context.SaveChangesAsync();
                    }

                    if (!context.CatalogItems.Any())
                    {
                        await context.CatalogItems.AddRangeAsync(CatalogItemsInit());
                        await context.SaveChangesAsync();
                    }
                }
                catch(Exception ex)
                {
                    _logger.Debug(ex.Message);
                }
            });
        }
        private static IEnumerable<CatalogBrand> CatalogBrandsInit()
        {
            IEnumerable<CatalogBrand> brands = new List<CatalogBrand>
            {
                new CatalogBrand{Brand = "Gucci"},
                new CatalogBrand{Brand = "Prada"},
                new CatalogBrand{Brand = "Balenciaga"},
                new CatalogBrand{Brand = "Marc Jacobs"},
                new CatalogBrand{Brand = "Pierre Cardin"},
                new CatalogBrand{Brand = "J.Crew"},
                new CatalogBrand{Brand = "Dolce & Gabbana"},
                new CatalogBrand{Brand = "AllSaints"},
                new CatalogBrand{Brand = "J-Brand"},
                new CatalogBrand{Brand = "Cheap Monday"},
                new CatalogBrand{Brand = "Fendi"},
                new CatalogBrand{Brand = "Burberry"},
                new CatalogBrand{Brand = "American Eagle Outfitters"},
                new CatalogBrand{Brand = "Moschino"},
                new CatalogBrand{Brand = "G-Star Raw"},
                new CatalogBrand{Brand = "Salvatore Ferragamo"},
                new CatalogBrand{Brand = "Coco Channel"},
                new CatalogBrand{Brand = "Calvin Klein"},
                new CatalogBrand{Brand = "Versace"},
                new CatalogBrand{Brand = "Ralph Lauren"},
                new CatalogBrand{Brand = "Christian Dior"},
                new CatalogBrand{Brand = "Yves Saint Laurent"},
                new CatalogBrand{Brand = "Christian Louboutin"},
                new CatalogBrand{Brand = "Karl Lagarfeld"},
                new CatalogBrand{Brand = "Roberto Cavalli"},
                new CatalogBrand{Brand = "Alexander McQueen"},
                new CatalogBrand{Brand = "Valentino"},
                new CatalogBrand{Brand = "Jean-Paul Gaultier"},
                new CatalogBrand{Brand = "Jimmy Choo"},
                new CatalogBrand{Brand = "Vera Wang"},
                new CatalogBrand{Brand = "Viviene Westwood"},
                new CatalogBrand{Brand = "Levis"},
                new CatalogBrand{Brand = "Diesel"},
                new CatalogBrand{Brand = "Hugo Boss"},
                new CatalogBrand{Brand = "Champion"}
            };

            return brands;
        }

        private static IEnumerable<CatalogType> CatalogTypesInit()
        {
            IEnumerable<CatalogType> types = new List<CatalogType>
            {
                new CatalogType{Type = "Shoes"},
                new CatalogType{Type = "Bags"},
                new CatalogType{Type = "Accessories"},
                new CatalogType{Type = "Vintage"},
                new CatalogType{Type = "Tops"},
                new CatalogType{Type = "Dresses"},
                new CatalogType{Type = "Jeans"},
                new CatalogType{Type = "Bottoms"},
                new CatalogType{Type = "T-Shirts"},
            };

            return types;
        }

        private static IEnumerable<CatalogItem> CatalogItemsInit()
        {
            IEnumerable<CatalogItem> items = new List<CatalogItem>
            {
                new CatalogItem{Name = "Gucci Dionysus Small suede and leather shoulder bag", Description = new String('a', 50),
                    Price = 1710.00M, PictureFilename = "Gucci_bag_1.png", CatalogTypeId = 1,
                    CatalogBrandId = 0, AvailableStock = 10, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},

                new CatalogItem{Name = "Balenciaga Classic City Textured-leather Tote - Black - one size", Description = new String('b', 50),
                    Price = 1045.00M, PictureFilename = "Balenciaga_bag_1.png", CatalogTypeId = 1,
                    CatalogBrandId = 2, AvailableStock = 5, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},

                new CatalogItem{Name = "Prada Pumps & High Heels", Description = new String('c', 50),
                    Price = 287.00M, PictureFilename = "Prada_shoes_1.png", CatalogTypeId = 0,
                    CatalogBrandId = 1, AvailableStock = 7, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},

                new CatalogItem{Name = "Marc Jacobs Lucille Platform Pumps", Description = new String('d', 50),
                    Price = 288.00M, PictureFilename = "MJacobs_shoes_1.png", CatalogTypeId = 0,
                    CatalogBrandId = 3, AvailableStock = 3, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},

                new CatalogItem{Name = "Pierre Cardin Acrylic Scarf Purple Women", Description = new String('e', 50),
                    Price = 30.00M, PictureFilename = "PCardin_accessory_1.png", CatalogTypeId = 2,
                    CatalogBrandId = 4, AvailableStock = 32, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = true},

                new CatalogItem{Name = "J.Crew Womens Ruffly Tulle Dress", Description = new String('f', 50),
                    Price = 98.00M, PictureFilename = "JCrew_dress_1.png", CatalogTypeId = 5,
                    CatalogBrandId = 5, AvailableStock = 12, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},

                new CatalogItem{Name = "Dolce & Gabbana Cotton-blend jacquard blouse", Description = new String('g', 50),
                    Price = 575.00M, PictureFilename = "DnB_top_1.png", CatalogTypeId = 4,
                    CatalogBrandId = 6, AvailableStock = 25, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},

                new CatalogItem{Name = "AllSaints Mina Top", Description = new String('h', 50),
                    Price = 65.00M, PictureFilename = "AllSaints_top_1.png", CatalogTypeId = 4,
                    CatalogBrandId = 7, AvailableStock = 25, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},

                new CatalogItem{Name = "J Brand Low Rise Ankle Cropped Skinny Jeans - Estate", Description = new String('i', 50),
                    Price = 95.00M, PictureFilename = "JBrand_jeans_1.png", CatalogTypeId = 6,
                    CatalogBrandId = 8, AvailableStock = 45, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = true},

                new CatalogItem{Name = "Cheap Monday Women's Dig Arched Logo Black T-Shirt", Description = new String('j', 50),
                    Price = 25.00M, PictureFilename = "CMonday_tee_1.png", CatalogTypeId = 8,
                    CatalogBrandId = 9, AvailableStock = 50, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},

                new CatalogItem{Name = "Vintage Champion Reverse Weave small logo pullover", Description = new String('k', 50),
                    Price = 27.50M, PictureFilename = "Vintage_Champion_top_1.png", CatalogTypeId = 3,
                    CatalogBrandId = 34, AvailableStock = 30, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},

                new CatalogItem{Name = "Fendi Fur-embellished Cotton-Blend Sweatshirt", Description = new String('l', 50),
                    Price = 522.00M, PictureFilename = "Fendi_top_1.png", CatalogTypeId = 4,
                    CatalogBrandId = 10, AvailableStock = 11, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},

                new CatalogItem{Name = "Burberry Flared Stretch Jeans-Size: 29-Black", Description = new String('m', 50),
                    Price = 295.00M, PictureFilename = "Burberry_jeans_1.png", CatalogTypeId = 6,
                    CatalogBrandId = 11, AvailableStock = 7, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},

                new CatalogItem{Name = "Balenciaga Low Top Logo Sneakers - White - IT 37", Description = new String('n', 50),
                    Price = 435.00M, PictureFilename = "Balenciaga_shoes_1.png", CatalogTypeId = 0,
                    CatalogBrandId = 2, AvailableStock = 21, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},
            };

            return items;
        }
    }
}