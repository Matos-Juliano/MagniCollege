using MagniCollege.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagniCollege.Data
{
    public class StudentRepo : IStudentRepo
    {
        private readonly CollegeContext _context;

        public StudentRepo(CollegeContext context)
        {
            _context = context;
        }

        public Task<List<Student>> GetStudents()
        {
            return Task.Run(() =>
            {
                return _context.Students.ToList();
            });
        }

        public Task<StudentDTO> GetStudentById(int id)
        {
            return Task.Run(() =>
            {
                return _context.Students.Select(x => new StudentDTO { Id = x.Id, Name = x.Name, Birthday = x.Birthday }).FirstOrDefault(x => x.Id == id);
            });
        }

        public Task<List<StudentDTO>> GetStudentsByName(string name)
        {
            return Task.Run(() =>
            {

                var query = from students in _context.Students
                            where students.Name.Contains(name)
                            select new StudentDTO
                            {
                                Id = students.Id,
                                Name = students.Name,
                                Birthday = students.Birthday
                            };

                return query.ToList();
            });
        }

        public Task<Student> AddNewStudent(Student student)
        {
            return Task.Run(() =>
            {
                var created = _context.Students.Add(student);
                if (created == null) throw new NotCreatedException("Could not add the student");
                _context.SaveChanges();
                return created;
            });
        }

        public Task<Student> UpdateStudent(AddAndUpdateStudentRequest student)
        {
            return Task.Run(() =>
            {
                Student toUpdate = _context.Students.FirstOrDefault(x => x.Id == student.StudentId);

                if (toUpdate == null) throw new NotCreatedException("Student not found");

                Student updated = toUpdate;

                if (!string.IsNullOrEmpty(student.Name)) updated.Name = student.Name;

                if (student.Birthday != null) updated.Birthday = (DateTime)student.Birthday;

                _context.Entry(toUpdate).CurrentValues.SetValues(updated);

                _context.SaveChanges();

                return updated;
            });
        }

        public Task<EnrollStudentResponse> EnrollStudent(int studentId, int courseId)
        {
            return Task.Run(() =>
            {
                var isenrolled = _context.CourseEnrollments.FirstOrDefault(x => x.Student.Id == studentId && x.IsEnrolled == true);

                if (isenrolled != null) throw new NotCreatedException("This student is already enrolled");

                List<string> errors = new List<string>();

                Course course = _context.Courses.FirstOrDefault(x => x.Id == courseId);

                if (course == null) errors.Add("The given Course is invalid");

                Student student = _context.Students.FirstOrDefault(x => x.Id == studentId);

                if (student == null) errors.Add("The given Student is invalid");

                if (errors.Count() > 0)
                {
                    if (errors.Count == 1) throw new NotCreatedException(errors[0] + ".");
                    else throw new NotCreatedException(errors[0] + " and " + errors[1] + ".");
                }

                CourseEnrollment enrollment = new CourseEnrollment
                {
                    Course = course,
                    Student = student,
                    IsEnrolled = true
                };

                var enrolled = _context.CourseEnrollments.Add(enrollment);

                if (enrolled == null)
                {
                    return new EnrollStudentResponse { Success = false };
                }

                _context.SaveChanges();

                return new EnrollStudentResponse { Success = true };
            });
        }

        public Task<EnrollStudentResponse> DisenrollStudent(int studentId, string comment)
        {
            return Task.Run(() =>
            {
                CourseEnrollment enrollment = _context.CourseEnrollments.FirstOrDefault(x => x.Student.Id == studentId && x.IsEnrolled == true);

                if (enrollment == null) throw new NotCreatedException("The informed student is not currently enrolled in any courses");

                Student student = _context.Students.FirstOrDefault(x => x.Id == studentId);

                var editedEnrollment = enrollment;

                editedEnrollment.IsEnrolled = false;
                //editedEnrollment.Student = student;

                _context.Entry(enrollment).CurrentValues.SetValues(editedEnrollment);

                DisenrollStudent disenrollStudent = new DisenrollStudent
                {
                    Student = student,
                    Comment = comment
                };

                var disenrolled = _context.DisenrollStudents.Add(disenrollStudent);

                _context.SaveChanges();

                return new EnrollStudentResponse { Success = true };
            });
        }

        public Task<List<GradesDTO>> GetAllGradesFromStudent(int studentId)
        {
            return Task.Run(() =>
            {
                return _context.Grades.Where(x => x.Student.Id == studentId).Select(x=> new GradesDTO { Student = x.Student.Name, Subject = x.Subject.Name, Value = x.Value}).ToList();
            });
        }
    
        public Task<CourseDTO> GetEnrolledCourse(int student)
        {
            return Task.Run(() =>
            {
                var query = from context in _context.CourseEnrollments
                            where context.Student.Id == student && context.IsEnrolled == true
                            join course in _context.Courses on context.Course.Id equals course.Id
                            select new CourseDTO
                            {
                                Id = course.Id,
                                Name = course.Name,
                            };

                return query.FirstOrDefault();
            });
            
        }
    }
}
