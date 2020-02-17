using System;
using System.Collections.Generic;

namespace Model
{
    public class StudentInfo
    {
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public List<SubjectMark> Marks { get;set; }
    }
}
