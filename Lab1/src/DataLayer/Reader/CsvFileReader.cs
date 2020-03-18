using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Model;

namespace FileReaders
{
    public class CsvFileReader : IReader
    {

        public IEnumerable<StudentMarksInfo> ReadFile(string path)
        {
            if (path == string.Empty || path == null)
                throw new ArgumentNullException();

            var records = new List<StudentMarksInfo>();


            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();

                var subjectNames = csv.Context.HeaderRecord.Skip(3).ToList();

                while (csv.Read())
                {
                    if (csv.Context.Record.Length != csv.Context.HeaderRecord.Length)
                        throw new InvalidDataException("File contains excess data");

                    var studentInfo = new StudentMarksInfo
                    {
                        FirstName = csv.GetField<string>(0),
                        Surname = csv.GetField<string>(1),
                        MiddleName = csv.GetField<string>(2)
                    };

                    var subjects = new List<Subject>();

                    subjectNames.ForEach(
                        name => subjects.Add(
                            new Subject()
                            {
                                Name = name,
                                Mark = csv.GetField<int>(name)
                            }));

                    studentInfo.Subjects = subjects.AsReadOnly();

                    records.Add(studentInfo);
                }
            }

            return records;
        }
    }
}
