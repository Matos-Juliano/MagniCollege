using MagniCollege.Data;
using MagniCollege.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MagniCollegeMigrate
{
    internal static class DbInitializator
    {
        internal static void DbInit()
        {
            var connection = GetConfiguredStrings();

            var context = new CollegeContext(connection[0].ConnectionString);

            var courses = GenerateCourses(context);

            if (courses.Count() <= 0) return;

            GenerateTeachers(context);

            GenerateSubjects(context);

            GenerateCourseSubjectDependencies(context);

            GenerateStudents(context);

            GenerateCourseEnrollment(context);

            GenerateGrades(context);
        }

        internal static List<ConnectionStrings> GetConfiguredStrings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: false);

            IConfiguration config = builder.Build();

            var result = config.GetSection(nameof(ConnectionStrings)).Get<List<ConnectionStrings>>();
            return result;
        }

        private static List<Course> GenerateCourses(CollegeContext context)
        {
            if (context.Courses.Count() > 0) return new List<Course>();
            List<Course> coursesToInit = new List<Course>
            {
                new Course{Id = 1,Name = "Architecture"},
                new Course{Id = 2,Name = "Aeronautics and Astronautics"},
                new Course{Id = 3,Name = "Biological Engineering"},
                new Course{Id = 4,Name = "Chemical Engineering"},
                new Course{Id = 5,Name = "Computer Science"}
            };

            List<Course> courses = new List<Course>();

            coursesToInit.ForEach(x =>
            {
                Course course = context.Courses.Add(x);
                courses.Add(course);
            });

            context.SaveChanges();

            return courses;
        } 

        private static void GenerateTeachers(CollegeContext context)
        {
            var culture = CultureInfo.CreateSpecificCulture("en-US");
            List<Teacher> teachersToAdd = new List<Teacher>
            {
                new Teacher{Id = 1,Name = "Steven Colbert",Birthday = System.DateTime.Parse("09/12/1981", culture),Salary = 2213.0}, //Design
                new Teacher{Id = 2,Name = "John Jones",Birthday = System.DateTime.Parse("07/22/1987", culture),Salary = 2100.0}, //Physics
                new Teacher{Id = 3,Name = "Johnny Maguire",Birthday = System.DateTime.Parse("01/19/1983", culture),Salary = 2213.0}, //Programming
                new Teacher{Id = 4,Name = "Steven Gerrard",Birthday = System.DateTime.Parse("03/07/1981", culture),Salary = 2150.0}, //Design
                new Teacher{Id = 5,Name = "George Clooney",Birthday = System.DateTime.Parse("08/11/1979", culture),Salary = 2450.0}, //Bioengineering
                new Teacher{Id = 6,Name = "Neil Jackson",Birthday = System.DateTime.Parse("04/05/1985", culture),Salary = 2100.0}, //Chemical Engineering
                new Teacher{Id = 7,Name = "Jack Douglas",Birthday = System.DateTime.Parse("11/11/1991", culture),Salary = 2213.0}, //Programming
                new Teacher{Id = 8,Name = "Felix Wilson",Birthday = System.DateTime.Parse("06/29/1988", culture),Salary = 2100.0}, //Bioengineering
                new Teacher{Id = 9,Name = "George Right",Birthday = System.DateTime.Parse("12/01/1980", culture),Salary = 2450.0} // Chemical Engineering
            };

            teachersToAdd.ForEach(x =>
            {
                context.Teachers.Add(x);
            });

            context.SaveChanges();
        }

        private static void GenerateSubjects(CollegeContext context)
        {
            List<Subject> subjectsToAdd = new List<Subject>
            {
                new Subject{Id = 1,Name = "Design Studio: How to Design",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 1)},
                new Subject{Id = 2,Name = "Design Studio: Introduction to Design Techniques & Technologies",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 4)},
                new Subject{Id = 3,Name = "Architecture Design Studio 1",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 1)},
                new Subject{Id = 4,Name = "Architecture Design Studio 2",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 1)},
                new Subject{Id = 5,Name = "Architecture Design Studio 3",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 1)},
                new Subject{Id = 6,Name = "materials and structures",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 2)},
                new Subject{Id = 7,Name = "fluids and aerodynamics",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 2)},
                new Subject{Id = 8,Name = "thermodynamics",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 2)},
                new Subject{Id = 9,Name = "physics and dynamics",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 2)},
                new Subject{Id = 10,Name = "computer programming",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 3)},
                new Subject{Id = 11,Name = "Introduction to Bioengineering",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 5)},
                new Subject{Id = 12,Name = "Statistical Thermodynamics of Biomolecular Systems",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 8)},
                new Subject{Id = 13,Name = "Introduction to Biological Engineering Design",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 8)},
                new Subject{Id = 14,Name = "Macroepidemiology",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 5)},
                new Subject{Id = 15,Name = "Introduction to Numerical Analysis for Engineering",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 9)},
                new Subject{Id = 16,Name = "Ethics for Engineers: Artificial Intelligence",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 6)},
                new Subject{Id = 17,Name = "Introduction to Sustainable Energy",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 6)},
                new Subject{Id = 18,Name = "Transport Processes",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 9)},
                new Subject{Id = 19,Name = "Separation Processes",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 6)},
                new Subject{Id = 20,Name = "Introduction to EECS via Communication Networks",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 3)},
                new Subject{Id = 21,Name = "Introduction to Computer Science Programming in Python",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 8)},
                new Subject{Id = 22,Name = "Introduction to Computational Thinking and Data Science",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 3)},
                new Subject{Id = 23,Name = "Mathematics for Computer Science",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 3)},
                new Subject{Id = 24,Name = "Introduction to Algorithms",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 3)},
                new Subject{Id = 25,Name = "Computation Structures",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 8)},
                new Subject{Id = 26,Name = "Fundamentals of Programming",Teacher = context.Teachers.FirstOrDefault(x=>x.Id == 8)}
            };

            subjectsToAdd.ForEach(x =>
            {
                context.Subjects.Add(x);
            });

            context.SaveChanges();
        }

        private static void GenerateCourseSubjectDependencies(CollegeContext context)
        {
            List<CourseSubjectDependency> courseSubjectDependencies = new List<CourseSubjectDependency>
            {
                new CourseSubjectDependency{Id = 1,Course = context.Courses.FirstOrDefault(x=> x.Name == "Architecture"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Design Studio: How to Design")},
                new CourseSubjectDependency{Id = 2,Course = context.Courses.FirstOrDefault(x=> x.Name == "Architecture"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Design Studio: Introduction to Design Techniques & Technologies")},
                new CourseSubjectDependency{Id = 3,Course = context.Courses.FirstOrDefault(x=> x.Name == "Architecture"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Architecture Design Studio 1")},
                new CourseSubjectDependency{Id = 4,Course = context.Courses.FirstOrDefault(x=> x.Name == "Architecture"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Architecture Design Studio 2")},
                new CourseSubjectDependency{Id = 5,Course = context.Courses.FirstOrDefault(x=> x.Name == "Architecture"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Architecture Design Studio 3")},
                new CourseSubjectDependency{Id = 6,Course = context.Courses.FirstOrDefault(x=> x.Name == "Aeronautics and Astronautics"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "materials and structures")},
                new CourseSubjectDependency{Id = 7,Course = context.Courses.FirstOrDefault(x=> x.Name == "Aeronautics and Astronautics"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "thermodynamics")},
                new CourseSubjectDependency{Id = 8,Course = context.Courses.FirstOrDefault(x=> x.Name == "Aeronautics and Astronautics"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "physics and dynamics")},
                new CourseSubjectDependency{Id = 9,Course = context.Courses.FirstOrDefault(x=> x.Name == "Aeronautics and Astronautics"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "computer programming")},
                new CourseSubjectDependency{Id = 10,Course = context.Courses.FirstOrDefault(x=> x.Name == "Aeronautics and Astronautics"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "fluids and aerodynamics")},
                new CourseSubjectDependency{Id = 11,Course = context.Courses.FirstOrDefault(x=> x.Name == "Biological Engineering"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Introduction to Bioengineering")},
                new CourseSubjectDependency{Id = 12,Course = context.Courses.FirstOrDefault(x=> x.Name == "Biological Engineering"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Statistical Thermodynamics of Biomolecular Systems")},
                new CourseSubjectDependency{Id = 13,Course = context.Courses.FirstOrDefault(x=> x.Name == "Biological Engineering"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Introduction to Biological Engineering Design")},
                new CourseSubjectDependency{Id = 14,Course = context.Courses.FirstOrDefault(x=> x.Name == "Biological Engineering"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Macroepidemiology")},
                new CourseSubjectDependency{Id = 15,Course = context.Courses.FirstOrDefault(x=> x.Name == "Chemical Engineering"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Introduction to Numerical Analysis for Engineering")},
                new CourseSubjectDependency{Id = 16,Course = context.Courses.FirstOrDefault(x=> x.Name == "Chemical Engineering"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Introduction to Sustainable Energy")},
                new CourseSubjectDependency{Id = 17,Course = context.Courses.FirstOrDefault(x=> x.Name == "Chemical Engineering"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Transport Processes")},
                new CourseSubjectDependency{Id = 18,Course = context.Courses.FirstOrDefault(x=> x.Name == "Chemical Engineering"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Separation Processes")},
                new CourseSubjectDependency{Id = 19,Course = context.Courses.FirstOrDefault(x=> x.Name == "Chemical Engineering"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Ethics for Engineers: Artificial Intelligence")},
                new CourseSubjectDependency{Id = 20,Course = context.Courses.FirstOrDefault(x=> x.Name == "Chemical Engineering"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "fluids and aerodynamics")},
                new CourseSubjectDependency{Id = 21,Course = context.Courses.FirstOrDefault(x=> x.Name == "Computer Science"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Introduction to EECS via Communication Networks")},
                new CourseSubjectDependency{Id = 22,Course = context.Courses.FirstOrDefault(x=> x.Name == "Computer Science"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Introduction to Computer Science Programming in Python")},
                new CourseSubjectDependency{Id = 23,Course = context.Courses.FirstOrDefault(x=> x.Name == "Computer Science"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Introduction to Computational Thinking and Data Science")},
                new CourseSubjectDependency{Id = 24,Course = context.Courses.FirstOrDefault(x=> x.Name == "Computer Science"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Mathematics for Computer Science")},
                new CourseSubjectDependency{Id = 25,Course = context.Courses.FirstOrDefault(x=> x.Name == "Computer Science"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Introduction to Algorithms")},
                new CourseSubjectDependency{Id = 26,Course = context.Courses.FirstOrDefault(x=> x.Name == "Computer Science"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Computation Structures")},
                new CourseSubjectDependency{Id = 27,Course = context.Courses.FirstOrDefault(x=> x.Name == "Computer Science"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Fundamentals of Programming")},
                new CourseSubjectDependency{Id = 28,Course = context.Courses.FirstOrDefault(x=> x.Name == "Computer Science"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "computer programming")}
            };

            courseSubjectDependencies.ForEach(x =>
            {
                context.CourseSubjectDependencies.Add(x);
            });

            context.SaveChanges();
        }
    
        private static void GenerateStudents(CollegeContext context)
        {
            var culture = CultureInfo.CreateSpecificCulture("en-US");
            List<Student> students = new List<Student>
            {
                new Student{Name = "Michael Robberts", Birthday = System.DateTime.Parse("03/12/1999", culture)},
                new Student{Name = "Sasha Simpson", Birthday = System.DateTime.Parse("09/26/2001", culture)},
                new Student{Name = "Robert Grey", Birthday = System.DateTime.Parse("05/01/2000", culture)},
                new Student{Name = "Mina Park", Birthday = System.DateTime.Parse("02/25/1999", culture)},
                new Student{Name = "Charles Eduards", Birthday = System.DateTime.Parse("11/21/1998", culture)},
                new Student{Name = "kim Leiner", Birthday = System.DateTime.Parse("03/14/2000", culture)},
                new Student{Name = "Marshal Walker", Birthday = System.DateTime.Parse("07/18/1998", culture)},
                new Student{Name = "Maya Uesaka", Birthday = System.DateTime.Parse("05/03/2001", culture)},
                new Student{Name = "Phill Matters", Birthday = System.DateTime.Parse("01/15/1999", culture)},
                new Student{Name = "John O'brien", Birthday = System.DateTime.Parse("06/22/2000", culture)}
            };

            students.ForEach(x =>
            {
                context.Students.Add(x);
            });

            context.SaveChanges();
        }

        private static void GenerateCourseEnrollment(CollegeContext context)
        {
            List<CourseEnrollment> courseEnrollments = new List<CourseEnrollment>
            {
                new CourseEnrollment{Id = 1,Course = context.Courses.FirstOrDefault(x=> x.Name == "Computer Science"),Student = context.Students.FirstOrDefault(x=> x.Name == "Michael Robberts"),IsEnrolled = true},
                new CourseEnrollment{Id = 2,Course = context.Courses.FirstOrDefault(x=> x.Name == "Computer Science"),Student = context.Students.FirstOrDefault(x=> x.Name == "Robert Grey"),IsEnrolled = true},
                new CourseEnrollment{Id = 3,Course = context.Courses.FirstOrDefault(x=> x.Name == "Computer Science"),Student = context.Students.FirstOrDefault(x=> x.Name == "Maya Uesaka"),IsEnrolled = true},
                new CourseEnrollment{Id = 4,Course = context.Courses.FirstOrDefault(x=> x.Name == "Aeronautics and Astronautics"),Student = context.Students.FirstOrDefault(x=> x.Name == "Mina Park"),IsEnrolled = true},
                new CourseEnrollment{Id = 5,Course = context.Courses.FirstOrDefault(x=> x.Name == "Aeronautics and Astronautics"),Student = context.Students.FirstOrDefault(x=> x.Name == "Phill Matters"),IsEnrolled = true},
                new CourseEnrollment{Id = 6,Course = context.Courses.FirstOrDefault(x=> x.Name == "Biological Engineering"),Student = context.Students.FirstOrDefault(x=> x.Name == "Marshal Walker"),IsEnrolled = true},
                new CourseEnrollment{Id = 7,Course = context.Courses.FirstOrDefault(x=> x.Name == "Chemical Engineering"),Student = context.Students.FirstOrDefault(x=> x.Name == "John O'brien"),IsEnrolled = true},
                new CourseEnrollment{Id = 8,Course = context.Courses.FirstOrDefault(x=> x.Name == "Chemical Engineering"),Student = context.Students.FirstOrDefault(x=> x.Name == "kim Leiner"),IsEnrolled = true},
                new CourseEnrollment{Id = 9,Course = context.Courses.FirstOrDefault(x=> x.Name == "Architecture"),Student = context.Students.FirstOrDefault(x=> x.Name == "Charles Eduards"),IsEnrolled = true},
                new CourseEnrollment{Id = 10,Course = context.Courses.FirstOrDefault(x=> x.Name == "Architecture"),Student = context.Students.FirstOrDefault(x=> x.Name == "Sasha Simpson"),IsEnrolled = true}
            };

            courseEnrollments.ForEach(x =>
            {
                context.CourseEnrollments.Add(x);
            });

            context.SaveChanges();
        }

        private static void GenerateGrades(CollegeContext context)
        {
            List<Grade> grades = new List<Grade>
            {
                new Grade{Id = 1,Student = context.Students.FirstOrDefault(x=> x.Name == "Sasha Simpson"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Design Studio: How to Design"),Value = 7.5},
                new Grade{Id = 2,Student = context.Students.FirstOrDefault(x=> x.Name == "Sasha Simpson"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Design Studio: Introduction to Design Techniques & Technologies"),Value = 7.2},
                new Grade{Id = 3,Student = context.Students.FirstOrDefault(x=> x.Name == "Sasha Simpson"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Architecture Design Studio 1"),Value = 8.5},
                new Grade{Id = 4,Student = context.Students.FirstOrDefault(x=> x.Name == "Charles Eduards"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Design Studio: How to Design"),Value = 7.0},
                new Grade{Id = 5,Student = context.Students.FirstOrDefault(x=> x.Name == "Charles Eduards"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Design Studio: Introduction to Design Techniques & Technologies"),Value = 8.4},
                new Grade{Id = 6,Student = context.Students.FirstOrDefault(x=> x.Name == "John O'brien"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Introduction to Numerical Analysis for Engineering"),Value = 7.9},
                new Grade{Id = 7,Student = context.Students.FirstOrDefault(x=> x.Name == "kim Leiner"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Introduction to Numerical Analysis for Engineering"),Value = 7.2},
                new Grade{Id = 8,Student = context.Students.FirstOrDefault(x=> x.Name == "kim Leiner"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Ethics for Engineers: Artificial Intelligence"),Value = 7.9},
                new Grade{Id = 9,Student = context.Students.FirstOrDefault(x=> x.Name == "Maya Uesaka"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Introduction to EECS via Communication Networks"),Value = 9.2},
                new Grade{Id = 10,Student = context.Students.FirstOrDefault(x=> x.Name == "Maya Uesaka"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Introduction to Algorithms"),Value = 8.4},
                new Grade{Id = 11,Student = context.Students.FirstOrDefault(x=> x.Name == "Maya Uesaka"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Mathematics for Computer Science"),Value = 8.9},
                new Grade{Id = 12,Student = context.Students.FirstOrDefault(x=> x.Name == "Robert Grey"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Introduction to Computational Thinking and Data Science"),Value = 8.9},
                new Grade{Id = 13,Student = context.Students.FirstOrDefault(x=> x.Name == "Robert Grey"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Introduction to Algorithms"),Value = 8.0},
                new Grade{Id = 14,Student = context.Students.FirstOrDefault(x=> x.Name == "Robert Grey"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Mathematics for Computer Science"),Value = 8.5},
                new Grade{Id = 15,Student = context.Students.FirstOrDefault(x=> x.Name == "Michael Robberts"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Introduction to Computer Science Programming in Python"),Value = 9.5},
                new Grade{Id = 16,Student = context.Students.FirstOrDefault(x=> x.Name == "Michael Robberts"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Introduction to Algorithms"),Value = 8.2},
                new Grade{Id = 17,Student = context.Students.FirstOrDefault(x=> x.Name == "Michael Robberts"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Mathematics for Computer Science"),Value = 8.9},
                new Grade{Id = 18,Student = context.Students.FirstOrDefault(x=> x.Name == "Mina Park"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "materials and structures"),Value = 8.7},
                new Grade{Id = 19,Student = context.Students.FirstOrDefault(x=> x.Name == "Mina Park"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "thermodynamics"),Value = 7.9},
                new Grade{Id = 20,Student = context.Students.FirstOrDefault(x=> x.Name == "Phill Matters"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "materials and structures"),Value = 8.2},
                new Grade{Id = 21,Student = context.Students.FirstOrDefault(x=> x.Name == "Phill Matters"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "thermodynamics"),Value = 8.6},
                new Grade{Id = 22,Student = context.Students.FirstOrDefault(x=> x.Name == "Marshal Walker"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Introduction to Bioengineering"),Value = 7.5},
                new Grade{Id = 23,Student = context.Students.FirstOrDefault(x=> x.Name == "Marshal Walker"),Subject = context.Subjects.FirstOrDefault(x=> x.Name == "Statistical Thermodynamics of Biomolecular Systems"),Value = 8.0}
            };

            grades.ForEach(x =>
            {
                context.Grades.Add(x);
            });

            context.SaveChanges();
        }
    }
}
