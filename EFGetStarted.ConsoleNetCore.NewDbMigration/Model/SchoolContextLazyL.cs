using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace EFGetStarted.ConsoleNetCore.NewDbMigration.Model
{
    public class SchoolContextLazyL : DbContext
    {
        //The samme context at SchoolContext but with Lazy Loading by Proxies enabled and no Logging
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<StudentAddress> StudentAddress { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Grade> Grades { get; set; }


      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.
            UseLazyLoadingProxies().//Enabling Lazy Loading by proxies
            UseSqlServer(@"Server=(localdb)\ProjectsV13;Database=SchoolDB;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Superflous statement in EF Core 2.1
            modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<Student>()
            .HasOne<StudentAddress>(s => s.Address)
            .WithOne(ad => ad.Student)
            .HasForeignKey<StudentAddress>(ad => ad.AddressOfStudentId);

        }


    }
}
