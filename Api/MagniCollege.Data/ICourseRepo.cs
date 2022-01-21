using MagniCollege.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagniCollege.Data
{
    public interface ICourseRepo
    {
        public Task<Course> AddCourse(Course course);
        public Task<CourseSubjectDependency> AddSubjectToCourse(int courseId, int subjectId);
        public Task<List<Course>> GetCourses();
        public Task<CourseDTO> GetCourseById(int courseId);
        Task<List<StudentDTO>> GetStudentsInCourse(int courseId);
        public Task<List<SubjectDTO>> GetSubjectsInCourse(int courseId);
        public Task<int> TeacherQuantityInCourse(int id);
        public Task<double> AverageGradesByCourse(int id);
        public Task<int> CountStudentsOnCourse(int id);
        public Task<bool> RemoveSubjectFromCourse(int id, int subjectId);
    }
}
