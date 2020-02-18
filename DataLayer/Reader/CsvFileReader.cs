using CsvHelper;
using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

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
                using (var csv = new CsvReader(reader,CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();

                    List<string> subjectNames = csv.Context.HeaderRecord.Skip(2).ToList();

                    while (csv.Read())
                    {
                        StudentMarksInfo studentInfo = new StudentMarksInfo
                        {
                            FirstName = csv.GetField<string>(0),
                            Surname = csv.GetField<string>(1),
                        };

                        List<Subject> subjects = new List<Subject>();

                        subjectNames.ForEach(name => subjects.Add(new Subject() { Name = name, Mark = csv.GetField<int>(name) }));

                        studentInfo.Subjects = subjects.AsReadOnly();
                        
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
