using Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileWriters
{
    public class ExcelWriter : IWriter
    {
        public void WriteToFile(GroupReport information, string path)
        {
            if (information == null || path == null)
                throw new ArgumentNullException();

            try
            {
                FileInfo excelFile = new FileInfo(path);

                using (ExcelPackage package = new ExcelPackage(excelFile))
                {
                    var worksheet = package.Workbook.Worksheets.Add($"{typeof(SummaryMarkInfo).ToString()}{package.Workbook.Worksheets.Count}");

                    var range = worksheet.Cells[1, 1];

                    range.LoadFromCollection(information.StudentAvegareInfos, true);

                    range.AutoFitColumns();

                    var lastRow = worksheet.Dimension.End.Row;

                    range = worksheet.Cells[lastRow + 2, 1];

                    range.LoadFromCollection(information.SummaryMarkInfo.Marks, true);

                    package.Save();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
