using Ohmyoga.Application.Models;

namespace Ohmyoga.Application.Services;

public interface ICourseService
{
    Task<bool> CreateAsync(Course course);

    Task<Course?> GetByIdAsync(Guid id);

    Task<Course?> GetBySlugAsync(string slug);

    Task<IEnumerable<Course>> GetAllAsync();

    Task<Course?> UpdateAsync(Course course);

    Task<bool> DeleteByIdAsync(Guid id);
}