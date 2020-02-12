using FileReaders;
using FileWriters;
using Logger;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class FileService
    {
        public void ConvertFile(string[] consoleArguments)
        {
            var reader = new CsvFileReader();
            var consoleHandler = new ConsoleHandler();
            var inputFileName = string.Empty;
            var outputFileName = string.Empty;
            Format format = Format.Json;

            try
            {
                consoleHandler.ParseConsoleArguments(consoleArguments, ref inputFileName, ref outputFileName, ref format);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex.Message);
            }

            IWriter writer = null;
            switch (format)
            {
                case Format.Json:
                    writer = new JsonWriter();
                    break;
                case Format.Xlsx:
                    writer = new ExcelWriter();
                    break;
                default:
                    break;
            }

            try
            {
                var fileProcessor = new FileProcessor(writer, reader);
                var infos = fileProcessor.ReadInfoFromFile(inputFileName);
                fileProcessor.WriteRecord(outputFileName, infos);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex.Message);
            }
        }
    }
}
