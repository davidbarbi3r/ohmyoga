using Ohmyoga.Application.Models;
using Ohmyoga.Contracts.Requests;
using Ohmyoga.Contracts.Responses;

namespace Ohmyoga.Api.Mapping;

// No need for third package to map
// Don't add business logic in it,
// Don't do fancy things in it
public static class ContractMapping
{
    public static Course MapToCourse(this CreateCourseRequest request)
    {
        return new Course
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Duration = request.Duration,
            Date = request.Date,
            Capacity = request.Capacity,
            CourseType = request.CourseType.ToList()
        };
    }
    
    public static Course MapToCourse(this UpdateCourseRequest request, Guid id)
    {
        return new Course
        {
            Id = id,
            Title = request.Title,
            Duration = request.Duration,
            Date = request.Date,
            Capacity = request.Capacity,
            CourseType = request.CourseType.ToList()
        };
    }

    public static CourseResponse MapToResponse(this Course course)
    {
        return new CourseResponse
        {
            Id = course.Id,
            Title = course.Title,
            Date = course.Date,
            Duration = course.Duration,
            Capacity = course.Capacity,
            CourseType = course.CourseType,
            Slug = course.Slug
        };
    }

    public static CoursesResponse MapToResponse(this IEnumerable<Course> courses)
    {
        return new CoursesResponse
        {
            Items = courses.Select(MapToResponse)
        };
    }
}
