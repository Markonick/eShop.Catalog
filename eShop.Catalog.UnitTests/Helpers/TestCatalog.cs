﻿using System;
using System.Collections.Generic;
using System.Linq;
using eShop.Catalog.Domain;

namespace eShop.Catalog.UnitTests.Helpers
{
    public static class TestCatalog
    {
        public static IEnumerable<CatalogBrand> CreateBrands()
        {
            IEnumerable<CatalogBrand> brands = new List<CatalogBrand>
            {
                new CatalogBrand{ Brand = "Gucci" },
                new CatalogBrand{ Brand = "Prada" },
                new CatalogBrand{ Brand = "Balenciaga" },
                new CatalogBrand{ Brand = "Marc Jacobs" },
                new CatalogBrand{ Brand = "Pierre Cardin" },
                new CatalogBrand{ Brand = "J.Crew" },
                new CatalogBrand{ Brand = "Dolce & Gabbana" },
                new CatalogBrand{ Brand = "AllSaints" },
                new CatalogBrand{ Brand = "J-Brand" },
                new CatalogBrand{ Brand = "Cheap Monday" },
                new CatalogBrand{ Brand = "Fendi" },
                new CatalogBrand{ Brand = "Burberry" },
                new CatalogBrand{ Brand = "American Eagle Outfitters" },
                new CatalogBrand{ Brand = "Moschino" },
                new CatalogBrand{ Brand = "G-Star Raw" },
                new CatalogBrand{ Brand = "Salvatore Ferragamo" },
                new CatalogBrand{ Brand = "Coco Channel" },
                new CatalogBrand{ Brand = "Calvin Klein" },
                new CatalogBrand{ Brand = "Versace" },
                new CatalogBrand{ Brand = "Ralph Lauren" },
                new CatalogBrand{ Brand = "Christian Dior" },
                new CatalogBrand{ Brand = "Yves Saint Laurent" },
                new CatalogBrand{ Brand = "Christian Louboutin" },
                new CatalogBrand{ Brand = "Karl Lagarfeld" },
                new CatalogBrand{ Brand = "Roberto Cavalli" },
                new CatalogBrand{ Brand = "Alexander McQueen" },
                new CatalogBrand{ Brand = "Valentino" },
                new CatalogBrand{ Brand = "Jean-Paul Gaultier" },
                new CatalogBrand{ Brand = "Jimmy Choo" },
                new CatalogBrand{ Brand = "Vera Wang" },
                new CatalogBrand{ Brand = "Viviene Westwood" },
                new CatalogBrand{ Brand = "Levis" },
                new CatalogBrand{ Brand = "Diesel" },
                new CatalogBrand{ Brand = "Hugo Boss" },
                new CatalogBrand{ Brand = "Champion" }
            };

            return brands;
        }

        public static IEnumerable<CatalogType> CreateTypes()
        {
            IEnumerable<CatalogType> types = new List<CatalogType>
            {
                new CatalogType{ Type = "Shoes" },
                new CatalogType{ Type = "Bags" },
                new CatalogType{ Type = "Accessories" },
                new CatalogType{ Type = "Vintage" },
                new CatalogType{ Type = "Tops" },
                new CatalogType{ Type = "Dresses" },
                new CatalogType{ Type = "Jeans" },
                new CatalogType{ Type = "Bottoms" },
                new CatalogType{ Type = "T-Shirts" },
            };

            return types;
        }

        public static CatalogResponse CreateItems()
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
                    CatalogBrandId = 3, AvailableStock = 21, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "AllSaints Jeans", Description = new String('o', 50),
                    Price = 135.00M, PictureFilename = "AllSaints_jeans_1.png", CatalogTypeId = 7,
                    CatalogBrandId = 8, AvailableStock = 40, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Vintage Levis Jeans", Description = new String('p', 50),
                    Price = 80.00M, PictureFilename = "Vintage_jeans_1.png", CatalogTypeId = 7,
                    CatalogBrandId = 32, AvailableStock = 10, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "J.Crew Top", Description = new String('q', 50),
                    Price = 190.00M, PictureFilename = "Fendi_jeans_1.png", CatalogTypeId = 5,
                    CatalogBrandId = 6, AvailableStock = 20, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Valentino Bag", Description = new String('r', 50),
                    Price = 780.00M, PictureFilename = "Valentino_bag_1.png", CatalogTypeId = 2,
                    CatalogBrandId = 27, AvailableStock = 5, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Diesel Top", Description = new String('w', 50),
                    Price = 44.40M, PictureFilename = "Diesel_top_1.png", CatalogTypeId = 5,
                    CatalogBrandId = 33, AvailableStock = 10, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Fendi Jeans", Description = new String('t', 50),
                    Price = 190.00M, PictureFilename = "Fendi_jeans_1.png", CatalogTypeId = 7,
                    CatalogBrandId = 11, AvailableStock = 20, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Roberto Cavalli Shoes", Description = new String('u', 50),
                    Price = 540.00M, PictureFilename = "Robertocavalli shoes_1.png", CatalogTypeId = 1,
                    CatalogBrandId = 25, AvailableStock = 20, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Burberry Scarf", Description = new String('v', 50),
                    Price = 60.00M, PictureFilename = "Burberry_scarf_1.png", CatalogTypeId = 3,
                    CatalogBrandId = 12, AvailableStock = 12, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Vercace Dress", Description = new String('w', 50),
                    Price = 369.00M, PictureFilename = "Vercace_dress_1.png", CatalogTypeId = 6,
                    CatalogBrandId = 19, AvailableStock = 20, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Fendi Shoes", Description = new String('x', 50),
                    Price = 190.00M, PictureFilename = "Fendi_shoes_1.png", CatalogTypeId = 1,
                    CatalogBrandId = 11, AvailableStock = 2, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Fendi Jeans", Description = new String('y', 42),
                    Price = 190.00M, PictureFilename = "Fendi_jeans_1.png", CatalogTypeId = 7,
                    CatalogBrandId = 11, AvailableStock = 20, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Fendi Jeans", Description = new String('z', 34),
                    Price = 190.00M, PictureFilename = "Fendi_jeans_1.png", CatalogTypeId = 7,
                    CatalogBrandId = 11, AvailableStock = 20, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Fendi Jeans", Description = new String('i', 25),
                    Price = 190.00M, PictureFilename = "Fendi_jeans_1.png", CatalogTypeId = 7,
                    CatalogBrandId = 11, AvailableStock = 20, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Vintage Jeans", Description = new String('e', 10),
                    Price = 190.00M, PictureFilename = "Vintage_jeans_2.png", CatalogTypeId = 4,
                    CatalogBrandId = 4, AvailableStock = 20, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Fendi Jeans", Description = new String('c', 70),
                    Price = 190.00M, PictureFilename = "Fendi_jeans_1.png", CatalogTypeId = 7,
                    CatalogBrandId = 11, AvailableStock = 20, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Jimmy Choo Shoes", Description = new String('s', 40),
                    Price = 190.00M, PictureFilename = "Jimmy_choo_shoes_1.png", CatalogTypeId = 1,
                    CatalogBrandId = 29, AvailableStock = 20, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Fendi Bag", Description = new String('f', 30),
                    Price = 190.00M, PictureFilename = "Fendi_bag_1.png", CatalogTypeId = 2,
                    CatalogBrandId = 11, AvailableStock = 20, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Diesel T-Shirt", Description = new String('a', 20),
                    Price = 190.00M, PictureFilename = "Fendi_jeans_1.png", CatalogTypeId = 9,
                    CatalogBrandId = 33, AvailableStock = 20, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Vintage Jeans", Description = new String('w', 50),
                    Price = 190.00M, PictureFilename = "Vintage_jeans_1.png", CatalogTypeId = 7,
                    CatalogBrandId = 11, AvailableStock = 20, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Fendi Top", Description = new String('x', 50),
                    Price = 190.00M, PictureFilename = "Fendi_top_1.png", CatalogTypeId = 5,
                    CatalogBrandId = 11, AvailableStock = 20, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Fendi Jeans", Description = new String('v', 50),
                    Price = 190.00M, PictureFilename = "Fendi_jeans_1.png", CatalogTypeId = 7,
                    CatalogBrandId = 11, AvailableStock = 20, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },

                new CatalogItem{ Name = "Champion Top", Description = new String('p', 50),
                    Price = 52.90M, PictureFilename = "Champion_top_1.png", CatalogTypeId = 5,
                    CatalogBrandId = 35, AvailableStock = 20, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false },
            };

            return new CatalogResponse {ItemsOnPage = items.ToList(), TotalItems = items.ToList().Count };
        }
    }
}