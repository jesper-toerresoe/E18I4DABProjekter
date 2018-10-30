using EFGetStarted.ConsoleNetCore.NewDbMigration.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EFGetStarted.ConsoleNetCore.NewDbMigration
{
    class Program
    {
        public static string GetName()
        {
            return "Bill Buffalo";
        }
        static void Main(string[] args)
        {
            //Basic query Linq to Entiy
            var context = new SchoolContext(); //Beware of debugger behavior!! May trigger reading from DB
            var studentsWithSameName = context.Students.Where(s => s.StudentName == GetName())
                                              .ToList();

            //Doing some INSERT
            using (context = new SchoolContext())
            {

                var std = new Student()
                {
                    StudentName = "Bill Buffalo",
                    GradeId =1
                };

                context.Students.Add(std);
                context.SaveChanges();

                var grd = new Grade()
                {
                    GradeName="GradGrade",
                    Section ="SectionSection"
                };
                var std1 = new Student()
                {
                    StudentName = "Sørine Sørensen",
                    Grade = grd
                };
                grd.Students.Add(std1);
                context.Students.Add(std1);
                context.Grades.Add(grd);
                context.SaveChanges();
            }

            //Eager Loading
            var context1 = new SchoolContext();

            var studentWithGrade = context1.Students
                                       .Where(s => s.StudentName == "Bill")
                                       .Include(s => s.Grade)
                                       .FirstOrDefault();
            //Multiple Eager loading
            var context2 = new SchoolContext();

            var studentWithGradeCourse = context2.Students.Where(s => s.StudentName == "Bill")
                                    .Include(s => s.Grade)
                                    .Include(s => s.StudentCourses)
                                    .FirstOrDefault();
            var context3 = new SchoolContext();
            var studentWithGradeCourseInfo = context3.Students.Where(s => s.StudentName == "Bill")
                                    .Include(s => s.Grade)
                                    .Include(s => s.StudentCourses)
                                    .ThenInclude(c => c.Course)
                                    .FirstOrDefault();

        }
    }
    
}
