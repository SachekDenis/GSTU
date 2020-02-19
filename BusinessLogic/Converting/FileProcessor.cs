using System;
using System.Collections.Generic;
using System.Linq;
using FileReaders;
using FileWriters;
using Model;

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

        public IEnumerable<StudentMarksInfo> ReadInfoFromFile(string inputFile)
        {
            if (inputFile == null || inputFile == string.Empty)
                throw new ArgumentNullException(nameof(inputFile));

            return reader.ReadFile(inputFile);

        }

        public void WriteRecord(string outputFile, IEnumerable<StudentMarksInfo> studentInfos)
        {

            if (outputFile == null || outputFile == string.Empty)
                throw new ArgumentNullException(nameof(outputFile));

            if (studentInfos == null)
                throw new ArgumentNullException(nameof(studentInfos));

            var studentTotals = studentInfos.CastToStudentAvegareInfo();
            var summaryMarkInfo = studentInfos.CastToSummaryMarkInfo();

            var groupReport = new GroupMarksReport()
            {
                SummaryMarkInfo = summaryMarkInfo,
                StudentAvegareInfos = studentTotals.ToList().AsReadOnly()
            };


            writer.WriteToFile(groupReport, outputFile);
        }
    }
}
