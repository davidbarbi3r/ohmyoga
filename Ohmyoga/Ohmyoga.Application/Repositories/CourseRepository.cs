using Ohmyoga.Application.Models;

namespace Ohmyoga.Application.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly List<Course> _courses = new List<Course>();
    public Task<bool> CreateAsync(Course course)
    {
        _courses.Add(course);
        return Task.FromResult(true);
    }

    public Task<Course?> GetByIdAsync(Guid id)
    {
        var course = _courses.SingleOrDefault(course => course.Id == id);
        return Task.FromResult(course);
    }

    public Task<IEnumerable<Course>> GetAllAsync()
    {
        return Task.FromResult(_courses.AsEnumerable());
    }

    public Task<bool> UpdateAsync(Course course)
    {
        var courseIndex = _courses.FindIndex(x => x.Id == course.Id);
        if (courseIndex == -1)
        {
            return Task.FromResult(false);
        }

        _courses[courseIndex] = course;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteByIdAsync(Guid id)
    {
        var removedCount = _courses.RemoveAll(course => course.Id == id);
        var courseRemoved = removedCount > 0;
        return Task.FromResult(courseRemoved);
    }
}