using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileWriters
{
    public interface IWriter
    {
        void WriteToFile(GroupReport info, string path);

    }
}
