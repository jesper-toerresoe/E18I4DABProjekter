using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFGetStarted.ConsoleNetCore.NewDbMigration.Model;
using Microsoft.EntityFrameworkCore;

namespace EFGetStarted.ConsoleNetCore.NewDbMigration
{
    

    class TestEFOptimisticLocking
    {
        /// <summary>
        /// Examples from https://msdn.microsoft.com/en-us/library/jj592904(v=vs.113).aspx
        /// </summary>

        public void OptmisticLockingReload()
        {
            using (var context = new SchoolContext())
            {
                var student = context.Students.Find(1); //Read DB entity into EF Context
                student.StudentName = "Donald"; //Change the EF Context Entity

                //Change the db behind the bag of the EF Context. Sents a SQL statement directly til RDBMS 
                context.Database.ExecuteSqlCommand(
                           "UPDATE dbo.Students SET StudentName = 'Buller45' WHERE StudentId = 1");
                bool saveFailed;
                do
                {
                    saveFailed = false;

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        saveFailed = true;

                        //Update the values of the entity that failed to save from the store
                        ex.Entries.Single().Reload();
                        
                    }
                    catch (DbUpdateException ex1)
                    {
                        saveFailed = true;
                        System.Console.WriteLine("I kill that dam'ed database: " + ex1.ToString());
                    }

                } while (saveFailed);
            }
        }

        public void optimisticLockingClientWins()
        {
            using (var context = new SchoolContext())
            {
                var student = context.Students.Find(1);
                student.StudentName = "Hillary";

                bool saveFailed;
                do
                {
                    saveFailed = false;
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        saveFailed = true;

                        // Update original values from the database 
                        var entry = ex.Entries.Single();
                        entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    }

                } while (saveFailed);
            }

        }
    }
}
