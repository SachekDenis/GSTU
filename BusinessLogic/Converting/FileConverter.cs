using FileReaders;
using FileWriters;
using System.Linq;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class FileConverter
    {
        private readonly IWriter writer;
        private readonly IReader reader;

        public FileConverter(IWriter writer, IReader reader)
        {
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        public IEnumerable<StudentMarksInfo> ReadInfoFromFile(string inputFile)
        {
            if (inputFile == null || inputFile == string.Empty)
                throw new ArgumentNullException(nameof(inputFile));

            try
            {
                return reader.ReadFile(inputFile);
            }
            catch
            {
                throw;
            }

        }

        public void WriteRecord(string outputFile, IEnumerable<StudentMarksInfo> studentInfos)
        {

            if (outputFile == null || outputFile == string.Empty)
                throw new ArgumentNullException(nameof(outputFile));

            if(studentInfos == null)
                throw new ArgumentNullException(nameof(studentInfos));

            var creator = new AverageMarksCreator();

            var studentTotals = creator.CastToStudentAvegareInfo(studentInfos);
            var summaryMarkInfo = creator.CastToSummaryMarkInfo(studentInfos);

            var groupReport = new GroupMarksReport()
            {
                SummaryMarkInfo = summaryMarkInfo,
                StudentAvegareInfos = studentTotals.ToList().AsReadOnly()
            };

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
