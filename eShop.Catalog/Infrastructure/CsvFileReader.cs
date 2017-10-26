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

        public IEnumerable<T> GetDataAsync()
        {
            using (var reader = new StreamReader(_path))
            {
                var csv = new CsvReader(reader);

                return ReadFile(csv);
            }
        }

        private static IEnumerable<T> ReadFile(IReader csv)
        {
            //csv.Configuration.HasHeaderRecord = false;
            csv.Configuration.MissingFieldFound = null;
            csv.Read();
            csv.ReadHeader();
            csv.Read();
            var x = csv.GetRecords<T>();
            return x;
            //csv.Read();
            //csv.ReadHeader();
            //return csv.GetRecords<T>();
        }
    }
}
