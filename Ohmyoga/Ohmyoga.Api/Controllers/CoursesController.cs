using Microsoft.AspNetCore.Mvc;
using Ohmyoga.Api;
using Ohmyoga.Api.Mapping;
using Ohmyoga.Application.Repositories;
using Ohmyoga.Contracts.Requests;

namespace Ohmyoga.RESTApi.Controllers;

[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ICourseRepository _courseRepository;

    public CoursesController(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    [HttpPost(ApiEndpoints.Courses.Create)]
    public async Task<IActionResult> Create([FromBody] CreateCourseRequest request)
    {
        var course = request.MapToCourse();
        await _courseRepository.CreateAsync(course);
        // that's a mistake, would better map course to a new CourseReponse object and return that
        // location header is not programaticly  given in this case
        // return Created($"/{ApiEndpoints.Courses.Create}/{course.Id}", course.MapToResponse());
        // with location header automatic 
        return CreatedAtAction(nameof(Get), new { id = course.Id }, course.MapToResponse());
    }

    [HttpGet(ApiEndpoints.Courses.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        if (course is null)
        {
            return NotFound();
        }

        return Ok(course.MapToResponse());
    }

    [HttpGet(ApiEndpoints.Courses.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var courses = await _courseRepository.GetAllAsync();
        var coursesResponse = courses.MapToResponse();
        return Ok(coursesResponse);
    }

    [HttpPut(ApiEndpoints.Courses.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCourseRequest request)
    {
        var course = request.MapToCourse(id);
        var updated = await _courseRepository.UpdateAsync(course);

        if (!updated)
        {
            return NotFound();
        }

        var response = course.MapToResponse();
        return Ok(response);
    }
}