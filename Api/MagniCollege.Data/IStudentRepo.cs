using MagniCollege.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagniCollege.Data
{
    public interface IStudentRepo
    {
        public Task<StudentDTO> GetStudentById(int id);
        public Task<List<Student>> GetStudents();
        public Task<List<StudentDTO>> GetStudentsByName(string name);
        public Task<Student> AddNewStudent(Student student);
        public Task<Student> UpdateStudent(AddAndUpdateStudentRequest student);
        public Task<EnrollStudentResponse> EnrollStudent(int studentId, int courseId);
        public Task<EnrollStudentResponse> DisenrollStudent(int studentId, string comment);
        public Task<List<GradesDTO>> GetAllGradesFromStudent(int studentId);
        public Task<CourseDTO> GetEnrolledCourse(int student);
    }
}
