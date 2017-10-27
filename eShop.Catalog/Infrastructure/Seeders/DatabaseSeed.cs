using System;
using System.Collections.Generic;
using eShop.Catalog.Domain;

namespace eShop.Catalog.Infrastructure.Seeders
{
    public class DatabaseSeed
    {
        public IEnumerable<CatalogBrand> CatalogBrandsInit()
        {
            IEnumerable<CatalogBrand> brands =  new List<CatalogBrand>
            {
                new CatalogBrand{Id = 0, Brand = "Gucci"},
                new CatalogBrand{Id = 1, Brand = "Prada"},
                new CatalogBrand{Id = 2, Brand = "Balenciaga"},
                new CatalogBrand{Id = 3, Brand = "Marc Jacobs"},
                new CatalogBrand{Id = 4, Brand = "Pierre Cardin"},
                new CatalogBrand{Id = 5, Brand = "J.Crew"},
                new CatalogBrand{Id = 6, Brand = "Dolce & Gabbana"},
                new CatalogBrand{Id = 7, Brand = "AllSaints"},
                new CatalogBrand{Id = 8, Brand = "J-Brand"},
                new CatalogBrand{Id = 9, Brand = "Cheap Monday"},
                new CatalogBrand{Id = 10, Brand = "Fendi"},
                new CatalogBrand{Id = 11, Brand = "Burberry"},
                new CatalogBrand{Id = 12, Brand = "American Eagle Outfitters"},
                new CatalogBrand{Id = 13, Brand = "Moschino"},
                new CatalogBrand{Id = 14, Brand = "G-Star Raw"},
                new CatalogBrand{Id = 15, Brand = "Salvatore Ferragamo"},
                new CatalogBrand{Id = 16, Brand = "Coco Channel"},
                new CatalogBrand{Id = 17, Brand = "Calvin Klein"},
                new CatalogBrand{Id = 18, Brand = "Versace"},
                new CatalogBrand{Id = 19, Brand = "Ralph Lauren"},
                new CatalogBrand{Id = 20, Brand = "Christian Dior"},
                new CatalogBrand{Id = 21, Brand = "Yves Saint Laurent"},
                new CatalogBrand{Id = 22, Brand = "Christian Louboutin"},
                new CatalogBrand{Id = 23, Brand = "Karl Lagarfeld"},
                new CatalogBrand{Id = 24, Brand = "Roberto Cavalli"},
                new CatalogBrand{Id = 25, Brand = "Alexander McQueen"},
                new CatalogBrand{Id = 26, Brand = "Valentino"},
                new CatalogBrand{Id = 27, Brand = "Jean-Paul Gaultier"},
                new CatalogBrand{Id = 28, Brand = "Jimmy Choo"},
                new CatalogBrand{Id = 29, Brand = "Vera Wang"},
                new CatalogBrand{Id = 30, Brand = "Viviene Westwood"},
                new CatalogBrand{Id = 31, Brand = "Levis"},
                new CatalogBrand{Id = 32, Brand = "Diesel"},
                new CatalogBrand{Id = 33, Brand = "Hugo Boss"},
                new CatalogBrand{Id = 34, Brand = "Champion"}
            };

            return brands;
        }

        public IEnumerable<CatalogType> CatalogTypesInit()
        {
            IEnumerable<CatalogType> types = new List<CatalogType>
            {
                new CatalogType{Id = 0, Type = "Shoes"},
                new CatalogType{Id = 1, Type = "Bags"},
                new CatalogType{Id = 2, Type = "Accessories"},
                new CatalogType{Id = 3, Type = "Vintage"},
                new CatalogType{Id = 4, Type = "Tops"},
                new CatalogType{Id = 5, Type = "Dresses"},
                new CatalogType{Id = 6, Type = "Jeans"},
                new CatalogType{Id = 7, Type = "Bottoms"},
                new CatalogType{Id = 8, Type = "T-Shirts"},
            };

            return types;
        }

        public IEnumerable<CatalogItem> CatalogItemsInit()
        {
            IEnumerable<CatalogItem> items = new List<CatalogItem>
            {
                new CatalogItem{Id = 0, Name = "Gucci Dionysus Small suede and leather shoulder bag", Description = new String('a', 50),
                    Price = 1710.00M, PictureFilename = "Gucci_bag_1.png", CatalogTypeId = 1, CatalogType = new CatalogType{Id = 1, Type = "Bags"},
                    CatalogBrandId = 0, CatalogBrand = new CatalogBrand{Id = 0, Brand = "Gucci"}, AvailableStock = 10, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},

                new CatalogItem{Id = 1, Name = "Balenciaga Classic City Textured-leather Tote - Black - one size", Description = new String('b', 50),
                    Price = 1045.00M, PictureFilename = "Balenciaga_bag_1.png", CatalogTypeId = 1, CatalogType = new CatalogType{Id = 1, Type = "Bags"},
                    CatalogBrandId = 2, CatalogBrand = new CatalogBrand{Id = 2, Brand = "Balenciaga"}, AvailableStock = 5, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},

                new CatalogItem{Id = 2, Name = "Prada Pumps & High Heels", Description = new String('c', 50),
                    Price = 287.00M, PictureFilename = "Prada_shoes_1.png", CatalogTypeId = 0, CatalogType = new CatalogType{Id = 0, Type = "Shoes"},
                    CatalogBrandId = 1, CatalogBrand = new CatalogBrand{Id = 1, Brand = "Prada"}, AvailableStock = 7, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},

                new CatalogItem{Id = 3, Name = "Marc Jacobs Lucille Platform Pumps", Description = new String('d', 50),
                    Price = 288.00M, PictureFilename = "MJacobs_shoes_1.png", CatalogTypeId = 0, CatalogType = new CatalogType{Id=0, Type = "Shoes"},
                    CatalogBrandId = 3, CatalogBrand = new CatalogBrand{Id = 3, Brand = "Marc Jacobs"}, AvailableStock = 3, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},

                new CatalogItem{Id = 4, Name = "Pierre Cardin Acrylic Scarf Purple Women", Description = new String('e', 50),
                    Price = 30.00M, PictureFilename = "PCardin_accessory_1.png", CatalogTypeId = 2, CatalogType = new CatalogType{Id = 2, Type = "Accessories"},
                    CatalogBrandId = 4, CatalogBrand = new CatalogBrand{Id = 4, Brand = "Pierre Cardin"}, AvailableStock = 32, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = true},

                new CatalogItem{Id = 5, Name = "J.Crew Womens Ruffly Tulle Dress", Description = new String('f', 50),
                    Price = 98.00M, PictureFilename = "JCrew_dress_1.png", CatalogTypeId = 5, CatalogType = new CatalogType{Id = 5, Type = "Dresses"},
                    CatalogBrandId = 5, CatalogBrand = new CatalogBrand{Id = 5, Brand = "J.Crew"}, AvailableStock = 12, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},

                new CatalogItem{Id = 6, Name = "Dolce & Gabbana Cotton-blend jacquard blouse", Description = new String('g', 50),
                    Price = 575.00M, PictureFilename = "DnB_top_1.png", CatalogTypeId = 4, CatalogType = new CatalogType{Id = 4, Type = "Tops"},
                    CatalogBrandId = 6, CatalogBrand = new CatalogBrand{Id = 6, Brand = "Dolce & Gabbana"}, AvailableStock = 25, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},

                new CatalogItem{Id = 7, Name = "AllSaints Mina Top", Description = new String('h', 50),
                    Price = 65.00M, PictureFilename = "AllSaints_top_1.png", CatalogTypeId = 4, CatalogType = new CatalogType{Id = 4, Type = "Tops"},
                    CatalogBrandId = 7, CatalogBrand = new CatalogBrand{Id = 7, Brand = "AllSaints"}, AvailableStock = 25, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},

                new CatalogItem{Id = 8, Name = "J Brand Low Rise Ankle Cropped Skinny Jeans - Estate", Description = new String('i', 50),
                    Price = 95.00M, PictureFilename = "JBrand_jeans_1.png", CatalogTypeId = 6, CatalogType = new CatalogType{Id = 6, Type = "Jeans"},
                    CatalogBrandId = 8, CatalogBrand = new CatalogBrand{Id = 8, Brand = "J Brand"}, AvailableStock = 45, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = true},

                new CatalogItem{Id = 9, Name = "Cheap Monday Women's Dig Arched Logo Black T-Shirt", Description = new String('j', 50),
                    Price = 25.00M, PictureFilename = "CMonday_tee_1.png", CatalogTypeId = 8, CatalogType = new CatalogType{Id = 8, Type = "T-Shirts"},
                    CatalogBrandId = 9, CatalogBrand = new CatalogBrand{Id = 9, Brand = "Cheap Monday"}, AvailableStock = 50, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},

                new CatalogItem{Id = 10, Name = "Vintage Champion Reverse Weave small logo pullover", Description = new String('k', 50),
                    Price = 27.50M, PictureFilename = "Vintage_Champion_top_1.png", CatalogTypeId = 3, CatalogType = new CatalogType{Id = 3, Type = "Vintage"},
                    CatalogBrandId = 34, CatalogBrand = new CatalogBrand{Id = 34, Brand = "Champion"}, AvailableStock = 30, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},

                new CatalogItem{Id = 11, Name = "Fendi Fur-embellished Cotton-Blend Sweatshirt", Description = new String('l', 50),
                    Price = 522.00M, PictureFilename = "Fendi_top_1.png", CatalogTypeId = 4, CatalogType = new CatalogType{Id = 4, Type = "Tops"},
                    CatalogBrandId = 10, CatalogBrand = new CatalogBrand{Id = 10, Brand = "Fendi"}, AvailableStock = 11, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},

                new CatalogItem{Id = 12, Name = "Burberry Flared Stretch Jeans-Size: 29-Black", Description = new String('m', 50),
                    Price = 295.00M, PictureFilename = "Burberry_jeans_1.png", CatalogTypeId = 6, CatalogType = new CatalogType{Id = 6, Type = "Jeans"},
                    CatalogBrandId = 11, CatalogBrand = new CatalogBrand{Id = 11, Brand = "Burberry"}, AvailableStock = 7, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},

                new CatalogItem{Id = 13, Name = "Balenciaga Low Top Logo Sneakers - White - IT 37", Description = new String('n', 50),
                    Price = 435.00M, PictureFilename = "Balenciaga_shoes_1.png", CatalogTypeId = 0, CatalogType = new CatalogType{Id = 0, Type = "Shoes"},
                    CatalogBrandId = 2, CatalogBrand = new CatalogBrand{Id = 2, Brand = "Balenciaga"}, AvailableStock = 21, DateTimeAdded = DateTime.Now, DateTimeModified = DateTime.Now, RestockThreshold = 50, OnReorder = false},
            };

            return items;
        }
    }
}
