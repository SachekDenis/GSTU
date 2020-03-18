using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public static class DataCaster
    {
        public static IEnumerable<StudentAvegareInfo> CastToStudentAvegareInfo(this IEnumerable<StudentMarksInfo> studentInfos)
        {
            return studentInfos.Select(studentInfo => new StudentAvegareInfo()
            {
                FirstName = studentInfo.FirstName,
                Surname = studentInfo.Surname,
                MiddleName = studentInfo.MiddleName,
                AverageMark = GetAverageMark(studentInfo)
            });
        }

        public static SummaryMarkInfo CastToSummaryMarkInfo(this IEnumerable<StudentMarksInfo> studentInfos)
        {
            var averageMarks = studentInfos
                .SelectMany(student => student.Subjects)
                .GroupBy(mark => mark.Name)
                .Select(subject => new Subject()
                {
                    Name = subject.Key,
                    Mark = subject.Average(subject => subject.Mark)
                }).ToList();

            averageMarks.Add(new Subject()
            {
                Name = "TotalAverageMark",
                Mark = averageMarks.Average(item => item.Mark)
            });

            var summaryMarkInfo = new SummaryMarkInfo()
            {
                AverageMarks = averageMarks.AsReadOnly()
            };

            return summaryMarkInfo;
        }

        private static double GetAverageMark(StudentMarksInfo studentInfo)
        {
            return studentInfo.Subjects
                .Sum(subject => subject.Mark)
                / studentInfo.Subjects.Count;
        }


    }
}
