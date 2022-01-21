using MagniCollege.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MagniCollege.Data
{
    public class CourseRepo : ICourseRepo
    {
        private readonly CollegeContext _context;

        public CourseRepo(CollegeContext context)
        {
            _context = context;
        }

        public Task<Course> AddCourse(Course course)
        {
            return Task.Run(() =>
            {
                var exists = _context.Courses.FirstOrDefault(x => x.Name.ToLower() == course.Name.ToLower());

                if (exists != null) throw new NotCreatedException("This course already exists!");

                var created = _context.Courses.Add(course);
                _context.SaveChanges();
                return created;
            });
        }

        public Task<List<Course>> GetCourses()
        {
            return Task.Run(() =>
            {
                return _context.Courses.ToList();
            });
        }

        public Task<CourseDTO> GetCourseById(int courseId)
        {
            return Task.Run(() =>
            {
                Course course = _context.Courses.FirstOrDefault(x => x.Id == courseId);

                if (course == null) throw new System.Exception("Could not find the course");

                var query = from courseSubjects in _context.CourseSubjectDependencies
                            where courseSubjects.Course.Id == courseId
                            join subject in _context.Subjects on courseSubjects.Subject.Id equals subject.Id into s
                            from subject in s.DefaultIfEmpty()
                            select new SubjectDTO
                            {
                                Id = subject.Id,
                                Name = subject.Name
                            };

                List<SubjectDTO> subjects = query.ToList();

                CourseDTO response = new CourseDTO
                {
                    Id = course.Id,
                    Name = course.Name,
                    Subjects = subjects
                };

                return response;
            });
        }

        public Task<CourseSubjectDependency> AddSubjectToCourse(int courseId, int subjectId)
        {
            return Task.Run(() =>
            {
                var exists = _context.CourseSubjectDependencies.FirstOrDefault(x => x.Course.Id == courseId && x.Subject.Id == subjectId);

                if (exists != null) throw new NotCreatedException("This subject is already in the course.");

                Course course = _context.Courses.FirstOrDefault(x => x.Id == courseId);

                if (course == null) throw new System.Exception("Could not find the course");

                Subject subject = _context.Subjects.FirstOrDefault(x => x.Id == subjectId);

                if (course == null) throw new System.Exception("Could not find the subject");


                CourseSubjectDependency courseSubjectDependency = new CourseSubjectDependency
                {
                    Course = course,
                    Subject = subject
                };

                CourseSubjectDependency created = _context.CourseSubjectDependencies.Add(courseSubjectDependency);
                _context.SaveChanges();

                return created;
            });
        }

        public Task<List<StudentDTO>> GetStudentsInCourse(int courseId)
        {
            return Task.Run(() =>
            {
                var query = from enrollments in _context.CourseEnrollments
                            join students in _context.Students on enrollments.Student.Id equals students.Id
                            where enrollments.Course.Id == courseId && enrollments.IsEnrolled == true
                            select new StudentDTO
                            {
                                Id = students.Id,
                                Name = students.Name,
                                Birthday = students.Birthday
                            };

                return query.ToList();
            });
        }

        public Task<List<SubjectDTO>> GetSubjectsInCourse(int courseId)
        {
            return Task.Run(() =>
            {
                var query = from subjectDepencies in _context.CourseSubjectDependencies
                            join subjects in _context.Subjects on subjectDepencies.Subject.Id equals subjects.Id
                            where subjectDepencies.Course.Id == courseId
                            select new SubjectDTO
                            {
                                Id = subjects.Id,
                                Name = subjects.Name,
                                Teacher = new TeacherDTO
                                {
                                    Id = subjects.Teacher.Id,
                                    Name = subjects.Teacher.Name,
                                    Birthday = subjects.Teacher.Birthday,
                                    Salary = subjects.Teacher.Salary
                                }
                            };

                return query.ToList();
            });
        }

        public Task<int> TeacherQuantityInCourse(int id)
        {
            return Task.Run(() =>
            {
                var query = (from subjectDependecies in _context.CourseSubjectDependencies
                             join subjects in _context.Subjects on subjectDependecies.Subject.Id equals subjects.Id
                             join teachers in _context.Teachers on subjects.Teacher.Id equals teachers.Id
                             where subjectDependecies.Course.Id == id
                             select new { Teacher = teachers.Id }).Distinct().ToList();

                return query.Count;
            });
        }        

        public Task<double> AverageGradesByCourse(int id)
        {
            return Task.Run(() =>
            {
                try
                {
                    var query = (from subjectDepenency in _context.CourseSubjectDependencies
                                 join subject in _context.Subjects on subjectDepenency.Subject.Id equals subject.Id
                                 join grade in _context.Grades on subject.Id equals grade.Subject.Id
                                 where subjectDepenency.Course.Id == id
                                 select new { Value = grade.Value }).Average(x => x.Value);

                    return query;
                }
                catch
                {
                    return 0.0;
                }
                
            });
        }
    
        public Task<int> CountStudentsOnCourse(int id)
        {
            return Task.Run(() =>
            {
                var query = (from enrollment in _context.CourseEnrollments
                             where enrollment.Course.Id == id && enrollment.IsEnrolled == true
                             select new { Id = enrollment.Id }).Count();

                return query;
            });
        }
    
        public Task<bool> RemoveSubjectFromCourse(int id, int subjectId)
        {
            return Task.Run(() =>
            {
                if (id == 0 || subjectId == 0) throw new NotCreatedException("Course Id and subjectId are required for this operation.");

                var dependency = _context.CourseSubjectDependencies.Where(x => x.Course.Id == id && x.Subject.Id == subjectId).FirstOrDefault();

                if (dependency == null) throw new NotCreatedException("This subject is not registered to this course.");

                _context.Entry(dependency).State = System.Data.Entity.EntityState.Deleted;

                _context.SaveChanges();

                return true;
            });            
        }
    }
}
