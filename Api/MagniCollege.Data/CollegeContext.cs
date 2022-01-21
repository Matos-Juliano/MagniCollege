using MagniCollege.Data.Migrations;
using MagniCollege.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagniCollege.Data
{
    public class CollegeContext : DbContext
    {
        public CollegeContext() : base()
        {
        }

        public CollegeContext(string connectionString) : base (connectionString)
        {
            
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<CourseEnrollment> CourseEnrollments { get; set; }
        public DbSet<CourseSubjectDependency> CourseSubjectDependencies { get; set; }
        public DbSet<DisenrollStudent> DisenrollStudents { get; set; }
    }
}
