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
            //Basic query Linq to EntiyFramework
            var context = new SchoolContext(); //Beware of debugger behavior!! May trigger reading from DB
            var studentsWithSameName = context.Students.Where(s => s.StudentName == GetName())
                                              .ToList();                          //Above is method GetName called

            //Doing some INSERT
            using (context = new SchoolContext())
            {

                var std = new Student() //No Scaffolding
                {
                    StudentName = "Bill Buffalo "+DateTime.UtcNow,
                    GradeId = 1   //Referencing by use of foregin key properties to grade ie. Grade ref is null
                };

                context.Students.Add(std);
                context.SaveChanges();

                //No handling of Primary and Foregin Key properties
                var grd = new Grade()
                {
                    GradeName = "GradGrade",
                    Section = "SectionSection"
                };
                var std1 = new Student()
                {
                    StudentName = "Sørine Sørensen "+DateTime.UtcNow,
                    Grade = grd  //Scaffold object std1 with object grd 
                };
                grd.Students.Add(std1); //Scaffold object grd with object std1
                context.Students.Add(std1);
                context.Grades.Add(grd);
                context.SaveChanges(); //And get properties with Primary and Foregin Keys updated to
            }

            //Eager Loading
            var context1 = new SchoolContext();

            var studentWithGrade = context1.Students
                                       .Where(s => s.StudentName == "Bill")
                                       .Include(s => s.Grade)
                                       .FirstOrDefault();
            //Multiple Eager loading 
            //The follwing three statments results in sending a request with two SELECT queries to the DB
            var context2 = new SchoolContext();

            var studentWithGradeCourse = context2.Students.Where(s => s.StudentName == "Sørine Sørensen") //First SELECT
                                    .Include(s => s.Grade) //First SELECT INNER JOIN
                                    .Include(s => s.StudentCourses) //Second SELECT INNER JOIN 
                                    .FirstOrDefault(); //Gives TOP 1 aggreation and triggers EF to send DB request

            var studentWithGradeCourseNoFiler = context2.Students//Where(s => s.StudentName == "Bill") //First SELECT
                                    .Include(s => s.Grade) //First SELECT INNER JOIN
                                    .Include(s => s.StudentCourses) //Second SELECT INNER JOIN 
                                    .ToList();

            var context3 = new SchoolContext();
            var studentWithGradeCourseInfo = context3.Students.Where(s => s.StudentName == "Bill")
                                    .Include(s => s.Grade) 
                                    .Include(s => s.StudentCourses)
                                    .ThenInclude(c => c.Course)
                                    .FirstOrDefault();
            //Projection Query Gives a new object with propertise according to Project
            var context4 = new SchoolContext();

            var stud = context4.Students.Where(s => s.StudentName == "Sørine Sørensen")
                                    .Select(s => new //Remember that Select is the Projection operator
                                    {
                                        Student = s, //A kind of "Root"
                                        Grade = s.Grade,//Use navigational reference properties. If name in new obejct stud also is Grade just use s.Grade
                                        GradeTeachers = s.Grade.Teachers //Use navigational collection properties
                                    })
                                    .FirstOrDefault();
            
            //Explicit Loading
            using (var context5 = new SchoolContext())
            {
                var student = context5.Students  //Setting up entry point (root!) for the given "context"
                                    .Where(s => s.StudentName == "Bill")
                                    .FirstOrDefault<Student>();

                // loads Student.Address into Context
                context5.Entry(student).Reference(s => s.Address).Load();
                // loads Courses collection into Context
                context5.Entry(student).Collection(s => s.StudentCourses).Load(); // loads Courses collection 
            }

            //Explicit Loading with filter (Querying)
            using (var context6 = new SchoolContext())
            {
                var student = context6.Students
                                    .Where(s => s.StudentName == "Bill")
                                    .FirstOrDefault<Student>();

                context6.Entry(student)
                       .Collection(s => s.StudentCourses) //Remember this is the M:N object
                       .Query()
                           .Where(sc => sc.Course.CourseName == "I4DAB") //Getting name through reference
                           .FirstOrDefault();
            } 
            //Use Lazy Loading
            using (var context7 = new SchoolContextLazyL())
            {


                // Display five blogs and select a blog
                var studentlist = context7.Students.Take(5);
                foreach (var c in studentlist)
                    Console.WriteLine(c.StudentId + " : " + c.StudentName);

                Console.WriteLine("Select a Student:");
                Int32 studID = Convert.ToInt32(Console.ReadLine());

                // Get a specified student by  id. 
                var speclist = context7.Students.Where(c => c.StudentId == studID).FirstOrDefault();

                // If lazy loading was not enabled no courses would be loaded for the student.
                foreach (var course in speclist.StudentCourses)
                {
                    Console.WriteLine("CourseID: {0} Title: {1} ",
                        course.CourseId, course.Course.CourseName);
                }
            }
            //The same sequence as above but without Lazy Loading
            using (var context8 = new SchoolContext())
            {
                //Test same scenario but now with lazy loading disabled
                // Display five blogs and select a blog
                var studentlist = context8.Students.Take(5);
                foreach (var c in studentlist)
                    Console.WriteLine(c.StudentId + " : " + c.StudentName);

                Console.WriteLine("Select a Student:");
                Int32 studID = Convert.ToInt32(Console.ReadLine());

                // Get a specified student by contact id. 
                var speclist = context8.Students.Where(c => c.StudentId == studID).FirstOrDefault();

                
                // Lazy loading is not enabled no Studentscourses will be loaded for the Student,
                // what will happen now?
                foreach (var course in speclist.StudentCourses)
                {
                    Console.WriteLine("CourseID: {0} Title: {1} ",
                        course.CourseId, course.Course.CourseName);
                }

                //Here comes "the saving angel should" have been done before "foreach"

                //var x = context8.StudentCourses.Where(s => s.StudentId == speclist.StudentId)                                    
                //                    .Include(c => c.Course)
                //                    .ToList();
                //This eager loading only load StudentCourse entitities, but not Course entities
                //context8.Entry(speclist).Collection(p => p.StudentCourses).Load();
            }

        }
    }
    
}
