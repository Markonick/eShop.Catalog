using System.Collections.Generic;

namespace eShop.Catalog.Domain
{
    public class CatalogResponse
    {
        public List<CatalogItem> ItemsOnPage { get; set; }
        public long TotalItems { get; set; }
    }
}