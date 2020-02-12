using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class AverageInfoCreator
    {
        public IEnumerable<StudentAvegareInfo> CastToStudentAvegareInfo(IEnumerable<StudentInfo> studentInfoes)
        {
            return studentInfoes.Select(e => new StudentAvegareInfo()
            {
                FirstName = e.FirstName,
                Surname = e.Surname,
                AverageMark = AverageMark(e)
            });
        }

        public SummaryMarkInfo CastToSummaryMarkInfo(IEnumerable<StudentInfo> studentInfoes)
        {
            var summaryMarkInfo = new SummaryMarkInfo()
            {
                Exams = new List<Exam>()
                {
                    new Exam() { Subject = Subject.Algoritmisation, Mark = studentInfoes.Average(e => e.MathMark) },
                    new Exam() { Subject = Subject.Drawing, Mark = studentInfoes.Average(e => e.DrawingMark) },
                    new Exam() { Subject = Subject.Math, Mark = studentInfoes.Average(e => e.MathMark) },
                    new Exam() { Subject = Subject.Physics, Mark = studentInfoes.Average(e => e.PhysicsMark) },
                    new Exam() { Subject = Subject.Chemistry, Mark = studentInfoes.Average(e => e.ChemistryMark) },
                    new Exam() { Subject = Subject.Average, Mark = studentInfoes.Average(e => AverageMark(e)) }
                },
                //AverageAlgoritmisationMark = studentInfoes.Average(e => e.AlgoritmisationMark),
                //AverageDrawingMark = studentInfoes.Average(e => e.DrawingMark),
                //AverageMathMark = studentInfoes.Average(e => e.MathMark),
                //AveragePhysicsMark = studentInfoes.Average(e => e.PhysicsMark),
                //AverageChemistryMark = studentInfoes.Average(e => e.ChemistryMark),
                //AverageMarkInGroup = studentInfoes.Average(e => AverageMark(e))
            };

            return summaryMarkInfo;
        }

        private double AverageMark(StudentInfo studentInfo)
        {
            return (studentInfo.MathMark
                + studentInfo.PhysicsMark
                + studentInfo.ChemistryMark
                + studentInfo.AlgoritmisationMark
                + studentInfo.DrawingMark) / 5.0;
        }


    }
}
