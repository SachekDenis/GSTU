using CsvHelper;
using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace FileReaders
{
    public class CsvFileReader : IReader
    {

        public IEnumerable<StudentMarksInfo> ReadFile(string path)
        {
            if (path == string.Empty || path == null)
                throw new ArgumentNullException();

            var records = new List<StudentMarksInfo>();

            try
            {
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
                {
                    csv.Read();
                    csv.ReadHeader();

                    List<string> subjectNames = csv.Context.HeaderRecord.Skip(2).ToList();

                    while (csv.Read())
                    {
                        StudentMarksInfo studentInfo = new StudentMarksInfo();
                        studentInfo.FirstName = csv.GetField<string>(0);
                        studentInfo.Surname = csv.GetField<string>(1);
                        studentInfo.Marks = new List<Subject>();
                        subjectNames.ForEach(name => studentInfo.Marks.Add(new Subject() { Name = name, Mark = csv.GetField<int>(name) }));
                        records.Add(studentInfo);
                    }
                }
            }
            catch
            {
                throw;
            }

            return records;
        }
    }
}
