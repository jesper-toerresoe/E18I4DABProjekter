using System;
using System.Collections.Generic;
using System.Text;

namespace EFGetStarted.ConsoleNetCore.NewDbMigration.Model
{
    public class Course
    {
      

        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }

}
