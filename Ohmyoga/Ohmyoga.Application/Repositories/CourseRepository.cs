using Ohmyoga.Application.Database;
using Ohmyoga.Application.Models;

namespace Ohmyoga.Application.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public CourseRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<bool> CreateAsync(Course course)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        {
            
        }
    }

    public Task<Course?> GetByIdAsync(Guid id)
    {
        
    }

    public Task<Course?> GetBySlugAsync(string slug)
    {
          
    }

    public Task<IEnumerable<Course>> GetAllAsync()
    {
        
    }

    public Task<bool> UpdateAsync(Course course)
    {
        
    }

    public Task<bool> DeleteByIdAsync(Guid id)
    {
        
    }
}