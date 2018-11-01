using System;
using System.Collections.Generic;
using System.Text;

namespace EFGetStarted.ConsoleNetCore.NewDbMigration.Model
{
    public class StudentCourse
    {
        public StudentCourse()
        {
            //Student = new Student();
        }

        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
