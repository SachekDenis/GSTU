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
            return studentInfos.Select(e => new StudentAvegareInfo()
            {
                FirstName = e.FirstName,
                Surname = e.Surname,
                AverageMark = AverageMark(e)
            });
        }

        public SummaryMarkInfo CastToSummaryMarkInfo(IEnumerable<StudentInfo> studentInfos)
        {
            var averageMarks = studentInfos
                .SelectMany(e => e.Marks)
                .GroupBy(x => x.Subject)
                .Select(e => new SubjectMark()
                {
                    Subject = e.Key,
                    Mark = e.Average(e => e.Mark)
                }).ToList();

            var summaryMarkInfo = new SummaryMarkInfo()
            {
                Marks = averageMarks.AsReadOnly()
            };

            return summaryMarkInfo;
        }

        private double AverageMark(StudentInfo studentInfo)
        {
            return studentInfo.Marks
                .Sum(e => e.Mark)
                / studentInfo.Marks.Count;
        }


    }
}
