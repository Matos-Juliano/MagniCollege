using MagniCollege.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagniCollege.Data
{
    public interface ITeachersRepo
    {
        public Task<List<TeacherDTO>> GetAllTeachers();
        public Task<List<TeacherDTO>> GetActiveTeachers();
        public Task<Teacher> AddNewTeacher(CreateAndUpdateTeacherRequest request);
        public Task<Teacher> UpdateTeacher(CreateAndUpdateTeacherRequest request);
        public Task<bool> ActivateTeacher(int teacherId);
        public Task<bool> DeActivateTeacher(int teacherId);
        public Task<TeacherDTO> GetTeacherById(int id);
        public Task<List<SubjectDTO>> GetTeachingSubjects(int id);
    }
}
