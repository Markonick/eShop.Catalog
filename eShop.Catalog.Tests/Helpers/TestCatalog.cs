using System;
using System.Collections.Generic;
using System.Linq;
using eShop.Catalog.Domain;

namespace eShop.Catalog.Tests.Helpers
{
    public static class TestCatalog
    {
       
        public static CatalogResponse Create()
        {
            IEnumerable<CatalogItem> items = new List<CatalogItem>
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

            return new CatalogResponse {ItemsOnPage = items.ToList(), TotalItems = items.ToList().Count };
        }
    }
}