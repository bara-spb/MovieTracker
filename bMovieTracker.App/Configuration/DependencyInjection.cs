using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace bMovieTracker.App.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureMovieTrackerApp(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MovieMappingProfile));
            services.AddTransient<IMovieTrackerService, MovieTrackerService>();
            return services;
        }
    }
}

