using MagniCollege.Data;
using MagniCollege.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagniCollege.Infrastructure
{
    public class CoursesService : ICoursesService
    {
        private readonly ICourseRepo _repo;

        public CoursesService(ICourseRepo coursesRepo)
        {
            _repo = coursesRepo;
        }
        public async Task<CourseDetailsView> GetCourseDetails(int id)
        {
            CourseDetailsView courseDetails = new CourseDetailsView();

            courseDetails.Course = await _repo.GetCourseById(id);

            courseDetails.Students = await _repo.GetStudentsInCourse(id);

            courseDetails.Subjects = await _repo.GetSubjectsInCourse(id);

            courseDetails.AverageGrade = await _repo.AverageGradesByCourse(id);

            courseDetails.TeachersTotal = await _repo.TeacherQuantityInCourse(id);

            return courseDetails;
        }

        public async Task<List<Course>> GetCoursesList()
        {
            List<Course> courses = await _repo.GetCourses();

            return courses;
        }
    }
}
