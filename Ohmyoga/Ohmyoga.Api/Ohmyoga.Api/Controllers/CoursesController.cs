using Microsoft.AspNetCore.Mvc;
using Ohmyoga.Api.Mapping;
using Ohmyoga.Application.Models;
using Ohmyoga.Application.Repositories;
using Ohmyoga.Contracts.Requests;

namespace Ohmyoga.Api.Controllers;

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
        return Created($"/{ApiEndpoints.Courses.Create}/{course.Id}", course);
    }
}