using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DataAccesLayer.File
{
    public class JsonReader<T> : IReader<T>
    {
        public T Read(string filename)
        {
            using (var reader = new StreamReader(filename))
            {
                var json = reader.ReadToEnd();
                return JsonSerializer.Deserialize<T>(json);
            }
        }
    }
}
