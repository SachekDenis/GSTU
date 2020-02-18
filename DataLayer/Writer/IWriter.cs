using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileWriters
{
    public interface IWriter
    {
        void WriteToFile(GroupMarksReport info, string path);

    }
}
