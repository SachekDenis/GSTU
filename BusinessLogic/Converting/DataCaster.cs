using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class DataCaster
    {
        public IEnumerable<StudentAvegareInfo> CastToStudentAvegareInfo(IEnumerable<StudentMarksInfo> studentInfos)
        {
            return studentInfos.Select(studentInfo => new StudentAvegareInfo()
            {
                FirstName = studentInfo.FirstName,
                Surname = studentInfo.Surname,
                AverageMark = AverageMark(studentInfo)
            });
        }

        public SummaryMarkInfo CastToSummaryMarkInfo(IEnumerable<StudentMarksInfo> studentInfos)
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

        private double AverageMark(StudentMarksInfo studentInfo)
        {
            return studentInfo.Subjects
                .Sum(subject => subject.Mark)
                / studentInfo.Subjects.Count;
        }


    }
}
