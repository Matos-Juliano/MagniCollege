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
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectRepo _repo;

        public SubjectsController(ISubjectRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllSubjects()
        {
            try
            {
                var subjects = await _repo.GetAllSubjects();
                return Ok(subjects);
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
                var result = await _repo.GetSubjectById(id);

                if(result != null)
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
        
        [HttpGet("{id}/teacher")]
        public async Task<IActionResult> GetTeacherFromSubject([FromRoute] int id)
        {
            try
            {
                var result = await _repo.GetTeacherFromSubject(id);

                if(result != null)
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

        [HttpGet("{id}/grade")]
        public async Task<IActionResult> GetGradeByStudentAndSubject([FromRoute] int id, [FromQuery] int studentId)
        {
            try
            {
                var result = await _repo.GetGradeByStudentAndSubject(id, studentId);

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

        [HttpGet("{id}/grades")]
        public async Task<IActionResult> GetGradeBySubject([FromRoute] int id)
        {
            try
            {
                var result = await _repo.GetGradesBySubject(id);

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
        public async Task<IActionResult> AddNewSubject(AddSubjectRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Name)) throw new NotCreatedException("The subject's name cannot be null.");

                var subject = await _repo.AddNewSubject(request);

                if (subject != null)
                {
                    return Ok(subject);
                }

                return NoContent();
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

        [HttpPost("{id}/grade")]
        public async Task<IActionResult> AddGradeToStudentAndSubject([FromRoute] int id, [FromBody] AddGradeRequest request)
        {
            try
            {
                if (request.StudentId <= 0) throw new NotCreatedException("The student Id is required for this procedure.");

                var subject = await _repo.AddGradeToStudentAndSubject(id, request.StudentId, request.Value);

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

        [HttpPost("{id}/teacher")]
        public async Task<IActionResult> UpdateTeacherFromSubject([FromRoute] int id, UpdateTeacherFromSubjectRequest request)
        {
            try
            {
                var result = await _repo.UpdateTeacherFromSubject(id, request.TeacherId);
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
