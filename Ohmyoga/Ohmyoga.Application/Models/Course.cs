using System.Text.RegularExpressions;

namespace Ohmyoga.Application.Models;

public partial class Course
{
    public required Guid Id { get; init; }
    
    public required string Title { get; set; }

    public string Slug => GenerateSlug();
    
    public required string Date { get; set; }
    
    public required int Capacity { get; set; }
    
    public required int Duration { get; set; }

    public required List<string> CourseType { get; init; } = new ();

    private string GenerateSlug()
    {
        var sluggedTitle = SlugRegex().Replace(Title, string.Empty)
                .ToLower().Replace(" ", "-");
        return $"{sluggedTitle}-{Date}";
    }
    // if this regex takes more than 5ms that means smb is trying sth dodgy
    [GeneratedRegex("[^0-9A-Za-z _-]", RegexOptions.NonBacktracking, 5)]
    private static partial Regex SlugRegex();
}