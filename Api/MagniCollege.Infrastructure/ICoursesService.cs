using MagniCollege.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagniCollege.Infrastructure
{
    public interface ICoursesService
    {
        public Task<List<Course>> GetCoursesList();

        public Task<CourseDetailsView> GetCourseDetails(int id);

    }
}
