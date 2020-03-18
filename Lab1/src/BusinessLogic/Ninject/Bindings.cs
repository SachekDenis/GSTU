using FileReaders;
using FileWriters;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    internal class Bindings : NinjectModule
    {
        private readonly Format _format;

        public Bindings(Format format)
        {
            _format = format;
        }

        public override void Load()
        {
            Bind<IReader>().To<CsvFileReader>();
            Bind<FileProcessor>().To<FileProcessor>().InSingletonScope();
            switch (_format)
            {
                case Format.Json:
                    Bind<IWriter>().To<JsonWriter>();
                    break;
                case Format.Xlsx:
                    Bind<IWriter>().To<ExcelWriter>();
                    break;
                default:
                    break;
            }
        }
    }
}
