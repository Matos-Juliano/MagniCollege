using MagniCollege.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagniCollege.Data
{
    public class TeachersRepo : ITeachersRepo
    {
        private readonly CollegeContext _context;

        public TeachersRepo(CollegeContext context)
        {
            _context = context;
        }

        public Task<List<TeacherDTO>> GetAllTeachers()
        {
            return Task.Run(() =>
            {
                var teachers = _context.Teachers.Select(x => new TeacherDTO { Id = x.Id, Name = x.Name, Birthday = x.Birthday, Salary = x.Salary }).ToList();

                return teachers;
            });
        }

        public Task<List<TeacherDTO>> GetActiveTeachers()
        {
            return Task.Run(() =>
            {
                return _context.Teachers.Where(x => x.IsActive == true).Select(x => new TeacherDTO { Id = x.Id, Name = x.Name, Birthday = x.Birthday, Salary = x.Salary }).ToList();
            });
        }

        public Task<TeacherDTO> GetTeacherById(int id)
        {
            return Task.Run(() =>
            {
                return _context.Teachers.Where(x => x.Id == id).Select(x => new TeacherDTO { Id = x.Id, Name = x.Name, Birthday = x.Birthday, Salary = x.Salary }).FirstOrDefault();
            });
        }

        public Task<Teacher> AddNewTeacher(CreateAndUpdateTeacherRequest request)
        {
            return Task.Run(() =>
            {
                var exists = _context.Teachers.FirstOrDefault(x => x.Name.ToLower() == request.Name.ToLower() && x.Birthday == request.Birthday);

                if (exists != null) throw new NotCreatedException("This teacher already exists");

                Teacher toCreate = new Teacher
                {
                    Name = request.Name,
                    Birthday = (DateTime)request.Birthday,
                    Salary = (double)request.Salary
                };

                if(request.IsActive != null) toCreate.IsActive = (bool)request.IsActive;

                var created = _context.Teachers.Add(toCreate);
                _context.SaveChanges();

                return created;
            });
        }

        public Task<Teacher> UpdateTeacher(CreateAndUpdateTeacherRequest request)
        {
            return Task.Run(() =>
            {
                var teacher = _context.Teachers.FirstOrDefault(x => x.Id == request.Id);

                if (teacher == null) throw new NotCreatedException("Could not find the teacher.");

                Teacher updated = teacher;

                if (!String.IsNullOrEmpty(request.Name)) updated.Name = request.Name;

                if (request.Birthday != null) updated.Birthday = (DateTime)request.Birthday;

                if (request.Salary != null && request.Salary > 0) updated.Salary = (double)request.Salary;

                _context.Entry(teacher).CurrentValues.SetValues(updated);

                _context.SaveChanges();

                return updated;
            });
        }

        public Task<bool> ActivateTeacher(int teacherId)
        {
            return Task.Run(() =>
            {
                var teacher = _context.Teachers.FirstOrDefault(x => x.Id == teacherId);

                if (teacher == null) throw new NotCreatedException("Could not find the teacher.");

                if (teacher.IsActive) throw new NotCreatedException("This teacher is already active");

                _context.Entry(teacher).CurrentValues.SetValues(new { IsActive = true });
                _context.SaveChanges();

                return true;
            });
        }

        public Task<bool> DeActivateTeacher(int teacherId)
        {
            return Task.Run(() =>
            {
                var teacher = _context.Teachers.FirstOrDefault(x => x.Id == teacherId);

                if (teacher == null) throw new NotCreatedException("Could not find the teacher.");

                if (!teacher.IsActive) throw new NotCreatedException("This teacher is not active.");

                _context.Entry(teacher).CurrentValues.SetValues(new { IsActive = false });
                _context.SaveChanges();

                return true;
            });
        }
    
        public Task<List<SubjectDTO>> GetTeachingSubjects(int id)
        {
            return Task.Run(() =>
            {
                var query = from subject in _context.Subjects
                            where subject.Teacher.Id == id
                            select new SubjectDTO { Id = subject.Id, Name = subject.Name };

                return query.ToList();
            });

        }
    }
}
