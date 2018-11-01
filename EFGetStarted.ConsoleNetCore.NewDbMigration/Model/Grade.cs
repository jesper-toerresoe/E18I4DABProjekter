using System;
using System.Collections.Generic;
using System.Text;

namespace EFGetStarted.ConsoleNetCore.NewDbMigration.Model
{
    public class Grade
    {
        public Grade()
        {
            //Students = new HashSet<Student>();
            //Teachers = new HashSet<Teacher>();
        }

        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public string Section { get; set; }

        public virtual ICollection<Student> Students { get; set; }    
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
