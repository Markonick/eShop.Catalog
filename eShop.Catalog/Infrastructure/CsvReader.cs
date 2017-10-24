using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.Catalog.Domain;

namespace eShop.Catalog.Infrastructure
{
    public class CsvReader<T> : ICsvReader<T> where T : class
    {
        public IEnumerable<T> GetData(string path)
        {
            throw new NotImplementedException();
        }
    }
}
