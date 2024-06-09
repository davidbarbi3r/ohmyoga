using Ohmyoga.Application.Models;

namespace Ohmyoga.Application.Repositories;

public interface ICourseRepository
{
    Task<bool> CreateAsync(Course course);
    // We may not use Course but CourseDto with a mapper if we're using a repository in a clean way
    Task<Course?> GetByIdAsync(Guid id);

    Task<Course?> GetBySlugAsync(string slug);

    Task<IEnumerable<Course>> GetAllAsync();

    Task<bool> UpdateAsync(Course course);

    Task<bool> DeleteByIdAsync(Guid id);
    
    Task<bool> ExistsByIdAsync(Guid id);

}