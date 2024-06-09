using Microsoft.AspNetCore.Mvc;
using Ohmyoga.Api.Mapping;
using Ohmyoga.Application.Repositories;
using Ohmyoga.Application.Services;
using Ohmyoga.Contracts.Requests;

namespace Ohmyoga.Api.Controllers;

[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpPost(ApiEndpoints.Courses.Create)]
    public async Task<IActionResult> Create([FromBody] CreateCourseRequest request)
    {
        var course = request.MapToCourse();
        await _courseService.CreateAsync(course);
        // that's a mistake, would better map course to a new CourseReponse object and return that
        // location header is not programaticly  given in this case
        // return Created($"/{ApiEndpoints.Courses.Create}/{course.Id}", course.MapToResponse());
        // with location header automatic 
        // created at endpoint get
        return CreatedAtAction(nameof(Get), new { idOrSlug = course.Id }, course.MapToResponse());
    }

    [HttpGet(ApiEndpoints.Courses.Get)]
    public async Task<IActionResult> Get([FromRoute] string idOrSlug)
    {
        var course = Guid.TryParse(idOrSlug, out var id) 
            ? await _courseService.GetByIdAsync(id)
            : await _courseService.GetBySlugAsync(idOrSlug);
            if (course is null)
            {
                return NotFound();
            }
            return Ok(course.MapToResponse());       
    }

    [HttpGet(ApiEndpoints.Courses.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var courses = await _courseService.GetAllAsync();
        var coursesResponse = courses.MapToResponse();
        return Ok(coursesResponse);
    }

    [HttpPut(ApiEndpoints.Courses.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCourseRequest request)
    {
        var course = request.MapToCourse(id);
        var updatedCourse = await _courseService.UpdateAsync(course);

        if (updatedCourse is null)
        {
            return NotFound();
        }

        var response = updatedCourse.MapToResponse();
        return Ok(response);
    }

    [HttpDelete(ApiEndpoints.Courses.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleted = await _courseService.DeleteByIdAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return Ok();
    }
}