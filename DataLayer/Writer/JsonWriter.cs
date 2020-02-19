using Model;
using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace FileWriters
{
    public class JsonWriter : IWriter
    {
        public void WriteToFile(GroupMarksReport information, string path)
        {
            if (information == null || path == null)
                throw new ArgumentNullException();

            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };

            string serializedCollection = JsonSerializer.Serialize(information, options);

            using var writer = new StreamWriter(path);
            writer.Write(serializedCollection);
        }
    }
}
