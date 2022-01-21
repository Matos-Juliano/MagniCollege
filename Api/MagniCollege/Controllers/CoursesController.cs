using MagniCollege.Data;
using MagniCollege.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MagniCollege.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepo _repo;

        public CoursesController(ICourseRepo coursesRepo)
        {
            _repo = coursesRepo;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCourses()
        {
            try
            {
                var courses = await _repo.GetCourses();

                if (courses.Count > 0)
                {
                    return Ok(courses);
                }

                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById([FromRoute]int id)
        {
            try
            {
                var course = await _repo.GetCourseById(id);

                if(course != null)
                {
                    return Ok(course);
                }

                return NoContent();

            }catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}/students")]
        public async Task<IActionResult> GetStudentsInCourse([FromRoute] int id)
        {
            try
            {
                var list = await _repo.GetStudentsInCourse(id);

                if (list != null)
                {
                    return Ok(list);
                }

                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}/teachercount")]
        public async Task<IActionResult> GetTeacherCountInCourse([FromRoute] int id)
        {
            try
            {
                var count = await _repo.TeacherQuantityInCourse(id);

                if (count > 0)
                {
                    return Ok(count);
                }

                return Ok(0);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}/gradeAverage")]
        public async Task<IActionResult> GetCourseGradeAverage([FromRoute] int id)
        {
            try
            {
                var count = await _repo.AverageGradesByCourse(id);

                if (count > 0)
                {
                    return Ok(count);
                }

                return Ok(0);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}/subjects")]
        public async Task<IActionResult> GetSubjectsInCourse([FromRoute] int id)
        {
            try
            {
                var list = await _repo.GetSubjectsInCourse(id);

                if (list != null)
                {
                    return Ok(list);
                }

                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewCourse(AddCourseRequest courseRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(courseRequest.Name)) throw new NotCreatedException("A Name must be provided.");

                Course course = new Course
                {
                    Name = courseRequest.Name
                };

                var created = await _repo.AddCourse(course);

                if (created == null) throw new NotCreatedException("The course was not created. Check your information and try again.");

                return Ok(created);
            }
            catch(NotCreatedException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("{id}/addSubject")]
        public async Task<IActionResult> AddSubjectToCourse([FromRoute] int id, [FromBody] AddSubjectToCourseRequest request)
        {
            try
            {
                var result = await _repo.AddSubjectToCourse(id, request.SubjectId);

                return Ok(result);
            }
            catch(NotCreatedException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    
        [HttpGet("{id}/studentcount")]
        public async Task<IActionResult> CountStudentsOnCourse([FromRoute]int id)
        {
            try
            {
                var count = await _repo.CountStudentsOnCourse(id);

                return Ok(count);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    
        [HttpDelete("{id}/subject")]
        public async Task<IActionResult> DeleteSubjectFromCourse([FromRoute]int id, [FromBody] AddSubjectToCourseRequest request)
        {
            try
            {
                var result = await _repo.RemoveSubjectFromCourse(id, request.SubjectId);

                return Ok(result);
            }
            catch(NotCreatedException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
