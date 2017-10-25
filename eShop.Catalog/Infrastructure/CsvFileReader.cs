using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;

namespace eShop.Catalog.Infrastructure
{
    public class CsvFileReader<T>
    {
        private readonly string _path;

        public CsvFileReader(string path)
        {
            _path = path;
        }

        public Task<IEnumerable<T>> GetDataAsync()
        {
            using (var reader = new StreamReader(_path))
            {
                var csv = new CsvReader(reader);

                return Task.Run(() => ReadFile(csv));
            }
        }

        private IEnumerable<T> ReadFile(CsvReader csv)
        {
            csv.Read();
            csv.ReadHeader();
            return csv.GetRecords<T>();
        }
    }
}
