using Model;
using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;

namespace FileWriters
{
    public class ExcelWriter : IWriter
    {
        private const int rowOffset = 2;
        public void WriteToFile(GroupMarksReport information, string path)
        {
            if (information == null || path == null)
                throw new ArgumentNullException();


            var excelFile = new FileInfo(path);

            using ExcelPackage package = new ExcelPackage(excelFile);
            
            if(package.Workbook.Worksheets.Count(worksheet=>worksheet.Name == typeof(SummaryMarkInfo).Name) > 0)
                package.Workbook.Worksheets.Delete(typeof(SummaryMarkInfo).Name);

            var worksheet = package.Workbook.Worksheets.Add(typeof(SummaryMarkInfo).Name);

            var range = worksheet.Cells[1, 1];

            range.LoadFromCollection(information.StudentAvegareInfos, true);

            range.AutoFitColumns();

            var lastRow = worksheet.Dimension.End.Row;

            range = worksheet.Cells[lastRow + rowOffset, 1];

            range.LoadFromCollection(information.SummaryMarkInfo.AverageMarks, true);

            package.Save();
        }
    }
}
