using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

            string serializedCollection = JsonSerializer.Serialize(information,options);

            try
            {
                File.WriteAllText(path, serializedCollection);
            }
            catch
            {
                throw;
            }
        }
    }
}
