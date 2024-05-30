namespace Ohmyoga.Application.Models;

public class Course
{
    public required Guid Id { get; init; }
    
    public required string Title { get; set; }
    
    public required string Date { get; set; }
    
    public required int Capacity { get; set; }
    
    public required int Duration { get; set; }

    public required List<string> CourseType { get; init; } = new ();
}