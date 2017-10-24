using System.Collections.Generic;

namespace eShop.Catalog.Domain
{
    public interface ICsvReader<T> where T : class
    {
        IEnumerable<T> GetData(string path);
    }
}