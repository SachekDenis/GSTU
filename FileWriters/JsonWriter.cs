using Logger;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace FileWriters
{
    public class JsonWriter : IWriter
    {
        public void WriteToFile(GroupReport information, string path)
        {
            if (information == null || path == null)
                throw new ArgumentNullException();

            string serializedCollection = JsonSerializer.Serialize(information);

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
