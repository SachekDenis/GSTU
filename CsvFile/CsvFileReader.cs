using CsvHelper;
using Logger;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace FileReaders
{
    public class CsvFileReader : IReader
    {
        public IEnumerable<T> ReadFile<T>(string path) where T : class
        {
            if (path == string.Empty || path == null)
                throw new ArgumentNullException();

            IEnumerable<T> records = null;

            try
            {
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    records = csv.GetRecords<T>().ToList();
                }
            }
            catch
            {
                throw;
            }

            return records;
        }
    }
}
