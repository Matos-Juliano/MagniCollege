using MagniCollege.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagniCollege.Data
{
    public interface ISubjectRepo
    {
        public Task<SubjectDTO> GetSubjectById(int id);
        public Task<List<SubjectDTO>> GetAllSubjects();
        public Task<Subject> AddNewSubject(AddSubjectRequest subject);
        public Task<Grade> AddGradeToStudentAndSubject(int subjectId, int studentId, double value);
        public Task<GradesDTO> GetGradeByStudentAndSubject(int subjectId, int studentId);
        public Task<List<GradesDTO>> GetGradesBySubject(int subjectId);
        public Task<bool> UpdateTeacherFromSubject(int subjectId, int teacherId);
        public Task<TeacherDTO> GetTeacherFromSubject(int subjectId);
    }
}
