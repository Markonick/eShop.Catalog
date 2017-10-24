using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;

namespace eShop.Catalog.Helpers
{
    public class FileReader<T>
    {
        private readonly string _path;

        public FileReader(string path)
        {
            _path = path;
        }

        public async Task<IEnumerable<T>> GetDataAsync()
        {
            using (TextReader reader = File.OpenText(_path))
            {
                var csv = new CsvReader(reader);
                await csv.ReadAsync();
                csv.ReadHeader();
                return csv.GetRecords<T>();
            }
        }
    }
}
