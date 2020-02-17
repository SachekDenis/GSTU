using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class AverageInfoCreator
    {
        public IEnumerable<StudentAvegareInfo> CastToStudentAvegareInfo(IEnumerable<StudentInfo> studentInfos)
        {
            return studentInfos.Select(studentInfo => new StudentAvegareInfo()
            {
                FirstName = studentInfo.FirstName,
                Surname = studentInfo.Surname,
                AverageMark = AverageMark(studentInfo)
            });
        }

        public SummaryMarkInfo CastToSummaryMarkInfo(IEnumerable<StudentInfo> studentInfos)
        {
            var averageMarks = studentInfos
                .SelectMany(student => student.Marks)
                .GroupBy(mark => mark.SubjectName)
                .Select(subject => new Subject()
                {
                    SubjectName = subject.Key,
                    Mark = subject.Average(subject => subject.Mark)
                }).ToList();

            averageMarks.Add(new Subject()
            {
                SubjectName = "TotalAverage",
                Mark = averageMarks.Average(item => item.Mark)
            });

            var summaryMarkInfo = new SummaryMarkInfo()
            {
                Marks = averageMarks.AsReadOnly()
            };

            return summaryMarkInfo;
        }

        private double AverageMark(StudentInfo studentInfo)
        {
            return studentInfo.Marks
                .Sum(subject => subject.Mark)
                / studentInfo.Marks.Count;
        }


    }
}
