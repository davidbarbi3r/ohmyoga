namespace Ohmyoga.Contracts.Responses;

public class CoursesResponse
{
    public IEnumerable<CourseResponse> Courses { get; init; } = Enumerable.Empty<CourseResponse>();
}