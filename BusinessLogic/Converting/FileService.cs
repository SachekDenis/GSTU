using FileReaders;
using FileWriters;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class FileService
    {

        public void ConvertFile(string[] consoleArguments)
        {
            var logger = LogManager.GetCurrentClassLogger();
            var reader = new CsvFileReader();
            var consoleHandler = new ConsoleHandler();
            var inputFileName = string.Empty;
            var outputFileName = string.Empty;
            var format = Format.Json;

            try
            {
                consoleHandler.ParseConsoleArguments(consoleArguments, ref inputFileName, ref outputFileName, ref format);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
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
                var fileProcessor = new FileConverter(writer, reader);
                var studentInfos = fileProcessor.ReadInfoFromFile(inputFileName);
                fileProcessor.WriteRecord(outputFileName, studentInfos);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
