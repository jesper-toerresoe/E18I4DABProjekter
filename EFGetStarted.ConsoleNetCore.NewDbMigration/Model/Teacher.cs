using System;
using System.Collections.Generic;
using System.Text;

namespace EFGetStarted.ConsoleNetCore.NewDbMigration.Model
{
    public class Teacher
    {
        public Teacher()
        {
        }

        public int TeacherID { get; set; }
        public string TeacherName { get; set; }
        public string TeacherType { get; set; }
        public int GradeId { get; set; }
        public virtual Grade Grade { get; set; }
    }
}
