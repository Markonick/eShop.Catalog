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
                new CatalogBrand{Id = 0, Brand = "Gucci"},new CatalogBrand{Id = 1, Brand = "Prada"},
                new CatalogBrand{Id = 2, Brand = "Balenciaga"},new CatalogBrand{Id = 3, Brand = "Marc Jacobs"},
                new CatalogBrand{Id = 4, Brand = "Pierre Cardin"},new CatalogBrand{Id = 5, Brand = "J.Crew"},
                new CatalogBrand{Id = 6, Brand = "Dolce & Gabbana"},new CatalogBrand{Id = 7, Brand = "All-Saints"},
                new CatalogBrand{Id = 8, Brand = "J-Brand"},new CatalogBrand{Id = 9, Brand = "Cheap Monday"},
                new CatalogBrand{Id = 10, Brand = "Fendi"},new CatalogBrand{Id = 11, Brand = "Burberry"},
                new CatalogBrand{Id = 12, Brand = "American Eagle Outfitters"},new CatalogBrand{Id = 13, Brand = "Moschino"},
                new CatalogBrand{Id = 14, Brand = "G-Star Raw"},new CatalogBrand{Id = 15, Brand = "Salvatore Ferragamo"},
                new CatalogBrand{Id = 16, Brand = "Coco Channel"},new CatalogBrand{Id = 17, Brand = "Calvin Klein"},
                new CatalogBrand{Id = 18, Brand = "Versace"},new CatalogBrand{Id = 19, Brand = "Ralph Lauren"},
                new CatalogBrand{Id = 20, Brand = "Christian Dior"},new CatalogBrand{Id = 21, Brand = "Yves Saint Laurent"},
                new CatalogBrand{Id = 22, Brand = "Christian Louboutin"},new CatalogBrand{Id = 23, Brand = "Karl Lagarfeld"},
                new CatalogBrand{Id = 24, Brand = "Roberto Cavalli"},new CatalogBrand{Id = 25, Brand = "Alexander McQueen"},
                new CatalogBrand{Id = 26, Brand = "Valentino"},new CatalogBrand{Id = 27, Brand = "Jean-Paul Gaultier"},
                new CatalogBrand{Id = 28, Brand = "Jimmy Choo"},new CatalogBrand{Id = 29, Brand = "Vera Wang"},
                new CatalogBrand{Id = 30, Brand = "Viviene Westwood"},new CatalogBrand{Id = 31, Brand = "Levis"},
                new CatalogBrand{Id = 32, Brand = "Diesel"},new CatalogBrand{Id = 33, Brand = "Hugo Boss"}
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

        public void CatalogItemsInit()
        {
            IEnumerable<CatalogItem> items = new List<CatalogItem>
            {
                new CatalogItem{Id = 0, Name = },
            };

            return items;
        }
    }
}
