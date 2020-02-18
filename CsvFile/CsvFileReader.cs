using CsvHelper;
using Logger;
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
        public IEnumerable<StudentInfo> ReadFile(string path)
        {
            if (path == string.Empty || path == null)
                throw new ArgumentNullException();

            var records = new List<StudentInfo>();

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
                        StudentInfo studentInfo = new StudentInfo();
                        studentInfo.FirstName = csv.GetField<string>(0);
                        studentInfo.Surname = csv.GetField<string>(1);
                        studentInfo.Marks = new List<SubjectMark>();
                        subjectNames.ForEach(name => studentInfo.Marks.Add(new SubjectMark() { Subject = name, Mark = csv.GetField<int>(name) }));
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
