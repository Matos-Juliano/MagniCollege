using MagniCollege.Data;
using MagniCollege.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagniCollege.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepo _repo;

        public StudentsController(IStudentRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var students = await _repo.GetStudents();
                return Ok(students);
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetByName([FromQuery]string name)
        {
            try
            {
                var students = await _repo.GetStudentsByName(name);
                return Ok(students);
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            try
            {
                var student = await _repo.GetStudentById(id);

                if(student != null)
                {
                    return Ok(student);
                }

                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewStudent([FromBody] AddAndUpdateStudentRequest student)
        {
            try
            {
                if (string.IsNullOrEmpty(student.Name) || student.Birthday == null) throw new NotCreatedException("Name and Birthday are required to create a student.");

                Student toCreate = new Student
                {
                    Name = student.Name,
                    Birthday = (DateTime)student.Birthday
                };

                var created = await _repo.AddNewStudent(toCreate);
                return Ok(created);
            }
            catch (NotCreatedException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch("{id}/edit")]
        public async Task<IActionResult> EditStudent([FromRoute] int id, [FromBody] AddAndUpdateStudentRequest request)
        {
            try
            {
                request.StudentId = id;

                var updated = await _repo.UpdateStudent(request);
                return Ok(updated);
            }
            catch (NotCreatedException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{id}/enroll")]
        public async Task<IActionResult> EnrollStudent([FromRoute]int id, EnrollDisenrollRequest request)
        {
            try
            {
                var created = await _repo.EnrollStudent(id, request.CourseId);
                return Ok(created);
            }
            catch(NotCreatedException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{id}/disenroll")]
        public async Task<IActionResult> DisenrollStudent([FromRoute] int id, [FromBody]EnrollDisenrollRequest request)
        {
            try
            {
                var created = await _repo.DisenrollStudent(id, request.Comment);
                return Ok(created);
            }
            catch (NotCreatedException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}/grades")]
        public async Task<IActionResult> GetAllGradesFromStudent([FromRoute] int id)
        {
            try
            {
                var grades = await _repo.GetAllGradesFromStudent(id);

                if(grades != null)
                {
                    return Ok(grades);
                }

                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    
        [HttpGet("{id}/course")]
        public async Task<IActionResult> GetEnrolledCourse([FromRoute] int id)
        {
            try
            {
                var response = await _repo.GetEnrolledCourse(id);

                if(response != null)
                {
                    return Ok(response);
                }

                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
