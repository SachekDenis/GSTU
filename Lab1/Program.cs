using BusinessLogic;
using System;

namespace ImformLab1
{
    class Program
    {
        static void Main(string[] args)
        {
            FileService fileService = new FileService();
            fileService.ConvertFile(args);
        }
    }
}
