using MagniCollege.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MagniCollege.Controllers
{
    [Route("v2/api/courses")]
    [ApiController]
    public class CoursesControllerV2 : ControllerBase
    {
        private readonly ICoursesService _coursesService;

        public CoursesControllerV2(ICoursesService coursesService)
        {
            _coursesService = coursesService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseView([FromRoute] int id)
        {
            try
            {                
                var courseView = await _coursesService.GetCourseDetails(id);

                return Ok(courseView);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetCoursesList()
        {
            try
            {
                var result = await _coursesService.GetCoursesList();

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
    }
}
