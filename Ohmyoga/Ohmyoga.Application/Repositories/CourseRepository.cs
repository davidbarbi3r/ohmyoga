using System.Text;
using Dapper;
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
        using var transaction = connection.BeginTransaction();

        var result = await connection.ExecuteAsync(new CommandDefinition("""
            insert into courses (id, title, slug, duration, capacity, date)
            values (@Id, @Title, @Slug, @Duration, @Capacity, @Date)
            """, course));

        if (result > 0)
        {
            foreach (var courseType in course.CourseType)
            {
                await connection.ExecuteAsync(new CommandDefinition("""
                    insert into courses_type (courseTypeId, name)
                    values (@CourseTypeId, @Name)
                    """,
                    new { courseTypeId = course.Id, Name = courseType }));
            }
        }
        transaction.Commit();

        return result > 0;
    }

    public async Task<Course?> GetByIdAsync(Guid id)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var course = await connection.QuerySingleOrDefaultAsync<Course>(
            new CommandDefinition("""
                    select * from courses where id = @id
                    """,
                new { id }));
        if (course is null)
        {
            return null;
        }

        var coursesType = await connection.QueryAsync<string>(
            new CommandDefinition("""
            select name from courses_type where courseTypeId = @id
            """, new { id = course.Id }));

        foreach (var courseType in coursesType)
        {
            course.CourseType.Add(courseType);
        }

        return course;
    }

    public async Task<Course?> GetBySlugAsync(string slug)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var course = await connection.QuerySingleOrDefaultAsync<Course>(
            new CommandDefinition("select * from courses where slug = @slug",
                new { slug }));
        if (course is null)
        {
            return null;
        }

        var coursesType = await connection.QueryAsync<string>(
            new CommandDefinition("""
            select name from courses_type where courseTypeId = @id
            """, new { id = course.Id }));

        foreach (var courseType in coursesType)
        {
            course.CourseType.Add(courseType);
        }

        return course;

    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var result = await connection.QueryAsync(new CommandDefinition("""
                select c.*, string_agg(t.name, ',') as courses_type
                from courses c left join courses_type t on c.id = t.coursetypeid
                group by id
                """));

        return result.Select(x => new Course
        {
            Id = x.id,
            Title = x.title,
            Date = x.date,
            Capacity = x.capacity,
            Duration = x.duration,
            CourseType = Enumerable.ToList(x.courses_type.Split(","))
        });
    }

    public async Task<bool> UpdateAsync(Course course)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        await connection.ExecuteAsync(new CommandDefinition("""
                                                            delete from courses_type where coursetypeid = @id
                                                            """, new { id = course.Id }));

        foreach (var type in course.CourseType)
        {
            await connection.ExecuteAsync(new CommandDefinition("""
                insert into courses_type (coursetypeid, name)
                values (@CourseId, @Name)
                """, new { CourseId = course.Id, Name = type }));
        }

        var result = await connection.ExecuteAsync(new CommandDefinition("""
            update courses set slug = @slug, title = @Title, duration = @Duration, capacity = @Capacity, date = @Date
            where id = @Id
            """, course));
        
        transaction.Commit();
        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();
        
        await connection.ExecuteAsync(new CommandDefinition("""
            delete from courses_type where coursetypeid = @id
            """, new { id }));

        var result = await connection.ExecuteAsync(new CommandDefinition("""
            delete from courses where id = @id
            """, new { id }));
        transaction.Commit();
        return result > 0;
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        return await connection.ExecuteScalarAsync<bool>(new CommandDefinition("""
            select count(1) from courses where id = @id
        """, new { id }));
    }
}