using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.File
{
    interface IReader<T>
    {
        T Read(string filename);
    }
}
