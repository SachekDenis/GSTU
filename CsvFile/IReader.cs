using System;
using System.Collections.Generic;
using System.Text;

namespace FileReaders
{
    public interface IReader
    {
        IEnumerable<T> ReadFile<T>(string path) where T : class;
    }
}
