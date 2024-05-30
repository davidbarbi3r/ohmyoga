using Ohmyoga.Application.Models;
using Ohmyoga.Contracts.Requests;

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
}