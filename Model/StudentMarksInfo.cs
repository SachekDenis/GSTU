using System;
using System.Collections.Generic;

namespace Model
{
    public class StudentMarksInfo
    {
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public IReadOnlyCollection<Subject> Subjects { get;set; }
    }
}
