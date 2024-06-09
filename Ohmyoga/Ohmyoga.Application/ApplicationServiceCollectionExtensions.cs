using Microsoft.Extensions.DependencyInjection;
using Ohmyoga.Application.Database;
using Ohmyoga.Application.Repositories;
using Ohmyoga.Application.Services;

namespace Ohmyoga.Application;

//This is just an abstraction
public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<ICourseRepository, CourseRepository>();
        services.AddSingleton<ICourseService, CourseService>();
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IDbConnectionFactory>(_ => new NpgsqlConnectionFactory(connectionString));
        services.AddSingleton<DbInitializer>();
        return services;
    }
}