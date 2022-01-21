using MagniCollege.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagniCollege.Data
{
    public class SubjectRepo : ISubjectRepo
    {
        private readonly CollegeContext _context;

        public SubjectRepo(CollegeContext collegeContext)
        {
            _context = collegeContext;
        }

        public Task<SubjectDTO> GetSubjectById(int id)
        {
            return Task.Run(() =>
            {
                var query = from subject in _context.Subjects
                            join teacher in _context.Teachers on subject.Teacher.Id equals teacher.Id into t
                            from teacher in t.DefaultIfEmpty()
                            where subject.Id == id
                            select new SubjectDTO
                            {
                                Id = subject.Id,
                                Name = subject.Name,
                                Teacher = new TeacherDTO
                                {
                                    Id = teacher.Id,
                                    Name = teacher.Name,
                                    Birthday = teacher.Birthday,
                                    Salary = teacher.Salary
                                }
                            };

                return query.FirstOrDefault();
            });
        }

        public Task<Subject> AddNewSubject(AddSubjectRequest request)
        {
            return Task.Run(() =>
            {
                var exists = _context.Subjects.FirstOrDefault(x => x.Name.ToLower() == request.Name.ToLower());

                if (exists != null) throw new NotCreatedException("The course already exists!");

                Subject subject = new Subject
                {
                    Name = request.Name
                };

                if(request.TeacherId != null)
                {
                    var teacher = _context.Teachers.FirstOrDefault(x => x.Id == (int)request.TeacherId);
                    if (teacher != null) subject.Teacher = teacher;
                }

                var created = _context.Subjects.Add(subject);
                _context.SaveChanges();
                return created;
            });
        }

        public Task<Grade> AddGradeToStudentAndSubject(int subjectId, int studentId, double value)
        {
            return Task.Run(() =>
            {
                if (value < 0 || value > 10) throw new Exception("The grade must be between 0 and 10");

                var subject = _context.Subjects.FirstOrDefault(x => x.Id == subjectId);

                if (subject == null) throw new Exception("Couldn't find the subject");

                var student = _context.Students.FirstOrDefault(x => x.Id == studentId);

                if (student == null) throw new Exception("Couldn't find the student");

                Grade grade = new Grade
                {
                    Subject = subject,
                    Student = student,
                    Value = value
                };

                var created = _context.Grades.Add(grade);
                _context.SaveChanges();

                return created;
            });
        }

        public Task<List<SubjectDTO>> GetAllSubjects()
        {
            return Task.Run(() =>
            {
                var query = from subject in _context.Subjects
                            join teacher in _context.Teachers on subject.Teacher.Id equals teacher.Id into t
                            from teacher in t.DefaultIfEmpty()
                            select new SubjectDTO
                            {
                                Id = subject.Id,
                                Name = subject.Name,
                                Teacher = new TeacherDTO
                                {
                                    Id = teacher.Id,
                                    Name = teacher.Name,
                                    Birthday = teacher.Birthday,
                                    Salary = teacher.Salary
                                }
                            };

                return query.ToList();
            });
        }

        public Task<GradesDTO> GetGradeByStudentAndSubject(int subjectId, int studentId)
        {
            return Task.Run(() =>
            {
                return _context.Grades.Where(x => x.Student.Id == studentId && x.Subject.Id == subjectId).Select(x => new GradesDTO { Student = x.Student.Name, Subject = x.Subject.Name, Value = x.Value }).FirstOrDefault();
            });
        }

        public Task<List<GradesDTO>> GetGradesBySubject(int subjectId)
        {
            return Task.Run(() =>
            {
                return _context.Grades.Where(x => x.Subject.Id == subjectId).Select(x=> new GradesDTO { Student = x.Student.Name, Subject = x.Subject.Name, Value = x.Value}).ToList();
            });
        }
    
        public Task<bool> UpdateTeacherFromSubject(int subjectId, int teacherId)
        {
            return Task.Run(() =>
            {
                List<string> errors = new List<string>();
                var subject = _context.Subjects.FirstOrDefault(x => x.Id == subjectId);

                if (subject == null) errors.Add("Could not find the subject");

                var teacher = _context.Teachers.FirstOrDefault(x => x.Id == teacherId);

                if (teacher == null) errors.Add("Could not find the teacher");

                if(errors.Count > 0)
                {
                    if (errors.Count == 1) throw new NotCreatedException(errors[0]);

                    throw new NotCreatedException(errors[0] + " and " + errors[1] + ".");
                }

                Subject updated = subject;
                updated.Teacher = teacher;

                _context.Entry(subject).CurrentValues.SetValues(updated);

                _context.SaveChanges();

                return true;
            });
        }
    
        public Task<TeacherDTO> GetTeacherFromSubject(int subjectId)
        {
            return Task.Run(() =>
            {
                var query = from subject in _context.Subjects
                            join teacher in _context.Teachers on subject.Teacher.Id equals teacher.Id
                            where subject.Id == subjectId
                            select new TeacherDTO
                            {
                                Id = teacher.Id,
                                Name = teacher.Name,
                                Birthday = teacher.Birthday,
                                Salary = teacher.Salary
                            };

                return query.FirstOrDefault();
            });
        }
    }
}
