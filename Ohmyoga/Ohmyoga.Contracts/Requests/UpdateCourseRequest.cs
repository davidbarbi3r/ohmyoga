namespace Ohmyoga.Contracts.Requests;

public class UpdateCourseRequest
{
    public required string Title { get; init; }
    
    public required string Date { get; set; }
    
    public required int Capacity { get; set; }
    
    public required int Duration { get; set; }

    public required IEnumerable<string> CourseType { get; init; } = Enumerable.Empty<string>();
}