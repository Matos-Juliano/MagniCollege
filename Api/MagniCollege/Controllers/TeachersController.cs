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
    public class TeachersController : ControllerBase
    {
        private readonly ITeachersRepo _repo;

        public TeachersController(ITeachersRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllTeachers()
        {
            try
            {
                var result = await _repo.GetAllTeachers();

                if (result != null)
                {
                    return Ok(result);
                }

                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("getactive")]
        public async Task<IActionResult> GetActiveTeachers()
        {
            try
            {
                var result = await _repo.GetActiveTeachers();

                if (result != null)
                {
                    return Ok(result);
                }

                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewTeacher(CreateAndUpdateTeacherRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Name)) throw new NotCreatedException("The name cannot be null.");
                if (request.Birthday == null) throw new NotCreatedException("The birthday cannot be null");
                if (request.Salary == null && request.Salary <= 0) throw new NotCreatedException("Please inform salary information");

                var subject = await _repo.AddNewTeacher(request);

                if (subject != null)
                {
                    return Ok(subject);
                }

                return NoContent();
            }
            catch (NotCreatedException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPatch("{id}/update")]
        public async Task<IActionResult> UpdateTeacher([FromRoute] int id, [FromBody]CreateAndUpdateTeacherRequest request)
        {
            try
            {
                var subject = await _repo.UpdateTeacher(request);

                if (subject != null)
                {
                    return Ok(subject);
                }

                return NoContent();
            }
            catch (NotCreatedException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPatch("{id}/activate")]
        public async Task<IActionResult> ActivateTeacher([FromRoute]int id)
        {
            try
            {
                var subject = await _repo.ActivateTeacher(id);

                if (subject != true)
                {
                    return Ok(subject);
                }

                return BadRequest();
            }
            catch (NotCreatedException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> DeactivateTeacher([FromRoute] int id)
        {
            try
            {
                var result = await _repo.DeActivateTeacher(id);

                if (result == true)
                {
                    return Ok(result);
                }

                return BadRequest();
            }
            catch (NotCreatedException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherById([FromRoute]int id)
        {
            try
            {
                var teacher = await _repo.GetTeacherById(id);

                if(teacher != null)
                {
                    return Ok(teacher);
                }

                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    
        [HttpGet("{id}/subjects")]
        public async Task<IActionResult> GetSubjectForTeacher([FromRoute]int id)
        {
            try
            {
                var list = await _repo.GetTeachingSubjects(id);

                if(list != null)
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
    }
}
