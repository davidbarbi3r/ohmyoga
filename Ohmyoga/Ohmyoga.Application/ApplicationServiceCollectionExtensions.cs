using Microsoft.Extensions.DependencyInjection;
using Ohmyoga.Application.Repositories;

namespace Ohmyoga.Application;

//This is just an abstraction
public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<ICourseRepository, CourseRepository>();
        return services;
    }
}