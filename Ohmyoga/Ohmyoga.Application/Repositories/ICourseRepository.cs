using Ohmyoga.Application.Models;

namespace Ohmyoga.Application.Repositories;

public interface ICourseRepository
{
    Task<bool> CreateAsync(Course course);

    Task<Course?> GetByIdAsync(Guid id);

    Task<Course?> GetBySlugAsync(string slug);

    Task<IEnumerable<Course>> GetAllAsync();

    Task<bool> UpdateAsync(Course course);

    Task<bool> DeleteByIdAsync(Guid id);
}