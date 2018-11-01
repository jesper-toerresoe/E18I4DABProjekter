using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFGetStarted.ConsoleNetCore.NewDbMigration.Model
{
    public class Student
    {
        public Student()
        {


            //Address = new StudentAddress();

            //Grade = new Grade();
            //StudentCourses = new HashSet<StudentCourse>();
        }

        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public virtual StudentAddress Address { get; set; }

        public int GradeId { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }

    }
}
