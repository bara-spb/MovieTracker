using bMovieTracker.App;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace bMovieTracker.Data.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureMovieTrackerData(this IServiceCollection services, string connectionString)
        {
            services.AddEntityFrameworkSqlServer();
            services.AddDbContextPool<MovieTrackerDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddTransient<IMoviesRepository, MoviesRepository>();
            return services;
        }
    }
}
