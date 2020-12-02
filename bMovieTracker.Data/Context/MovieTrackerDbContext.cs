using bMovieTracker.Data.Configurations;
using bMovieTracker.Domain;
using Microsoft.EntityFrameworkCore;

namespace bMovieTracker.Data
{
    public class MovieTrackerDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public MovieTrackerDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new MovieConfiguration(modelBuilder.Entity<Movie>());
            MovieTrackerDbInitializer.Initialize(modelBuilder);
        }
    }
}
