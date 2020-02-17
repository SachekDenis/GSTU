using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileReaders
{
    public interface IReader
    {
        IEnumerable<StudentInfo> ReadFile(string path);
    }
}
