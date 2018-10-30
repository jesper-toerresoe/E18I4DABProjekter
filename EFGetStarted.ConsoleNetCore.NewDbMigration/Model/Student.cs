using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFGetStarted.ConsoleNetCore.NewDbMigration.Model
{
    public class Student
    {

        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public StudentAddress Address { get; set; }

        public int GradeId { get; set; }
        public Grade Grade { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }

    }
}
