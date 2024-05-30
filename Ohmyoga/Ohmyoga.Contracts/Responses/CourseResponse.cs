namespace Ohmyoga.Contracts.Responses;

public class CourseResponse
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    
    public required string Date { get; set; }
    
    public required int Capacity { get; set; }
    
    public required int Duration { get; set; }

    public required IEnumerable<string> CourseType { get; init; } = Enumerable.Empty<string>();
}