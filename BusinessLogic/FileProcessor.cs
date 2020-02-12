using FileReaders;
using FileWriters;
using Logger;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class FileProcessor
    {
        private readonly IWriter writer;
        private readonly IReader reader;

        public FileProcessor(IWriter writer, IReader reader)
        {
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        public IEnumerable<StudentInfo> ReadInfoFromFile(string inputFile)
        {
            if (inputFile == null || inputFile == string.Empty)
                throw new ArgumentNullException();

            try
            {
                return reader.ReadFile<StudentInfo>(inputFile);
            }
            catch
            {
                throw;
            }

        }

        public void WriteRecord(string outputFile, IEnumerable<StudentInfo> studentInfos)
        {

            if (outputFile == null || outputFile == string.Empty)
                throw new ArgumentNullException();

            var creator = new AverageInfoCreator();

            var studentTotals = creator.CastToStudentAvegareInfo(studentInfos);
            var summaryMarkInfo = creator.CastToSummaryMarkInfo(studentInfos);

            var groupReport = new GroupReport() { SummaryMarkInfo = summaryMarkInfo, StudentAvegareInfos = studentTotals };

            try
            {
                writer.WriteToFile(groupReport, outputFile);
            }
            catch
            {
                throw;
            }
        }
    }
}
