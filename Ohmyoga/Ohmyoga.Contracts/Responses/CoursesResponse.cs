namespace Ohmyoga.Contracts.Responses;

public class CoursesResponse
{
    public IEnumerable<CourseResponse> Items { get; init; } = Enumerable.Empty<CourseResponse>();
}