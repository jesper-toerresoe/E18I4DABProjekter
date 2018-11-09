using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFGetStarted.ConsoleNetCore.NewDbMigration.Model;
using Microsoft.EntityFrameworkCore;

namespace EFGetStarted.ConsoleNetCore.NewDbMigration
{
    class ConcurrencyCheck
    {

        public void TestConcurrencyException()
        {
            Student student = null;

            using (var context = new SchoolContext())
            {
                student = context.Students.First();
            }

            //Edit student name
            Console.Write("Enter New Student Name:");
            student.StudentName = Console.ReadLine(); //Assigns student name from console

            using (var context = new SchoolContext())
            {
                try
                {
                    context.Entry(student).State = EntityState.Modified;
                    context.SaveChanges();

                    Console.WriteLine("Student saved successfully.");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine("Concurrency Exception Occurred.");
                }
            }

        }
    }
}
