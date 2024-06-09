using Ohmyoga.Application.Models;
using Ohmyoga.Application.Repositories;

namespace Ohmyoga.Application.Services;

public class CourseService: ICourseService
{
    private readonly ICourseRepository _courseRepository;
    
    public Task<bool> CreateAsync(Course course)
    {
        return _courseRepository.CreateAsync(course);
    }

    public Task<Course?> GetByIdAsync(Guid id)
    {
        return _courseRepository.GetByIdAsync(id);
    }

    public Task<Course?> GetBySlugAsync(string slug)
    {
        return _courseRepository.GetBySlugAsync(slug);
    }

    public Task<IEnumerable<Course>> GetAllAsync()
    {
       return _courseRepository.GetAllAsync();
    }

    public async Task<Course?> UpdateAsync(Course course)
    {
        var courseExists = await _courseRepository.ExistsByIdAsync(course.Id);
        if (!courseExists)
        {
            return null;
        }

        await _courseRepository.UpdateAsync(course);
        return course;
    }

    public Task<bool> DeleteByIdAsync(Guid id)
    {
        return _courseRepository.DeleteByIdAsync(id);
    }
}