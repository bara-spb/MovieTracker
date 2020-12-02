using bMovieTracker.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace bMovieTracker.Data
{
    public class MovieTrackerDbInitializer
    {
        public static void Initialize(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(
                new Movie {
                    Id = 1,
                    Title = "The Green Mile",
                    Year = new ReleaseYear(1999),
                    Genre = GenreTypes.Drama,
                    Rate = RateTypes.FiveStars },
                new Movie {
                    Id = 2,
                    Title = "Interstellar",
                    Year = new ReleaseYear(2014),
                    Genre = GenreTypes.Fantasy,
                    Rate = RateTypes.FiveStars },
                new Movie
                {
                    Id = 3,
                    Title = "The Lion King",
                    Year = new ReleaseYear(1994),
                    Genre = GenreTypes.Animation,
                    Rate = RateTypes.FiveStars
                },
                new Movie
                {
                    Id = 4,
                    Title = "The Matrix",
                    Year = new ReleaseYear(1999),
                    Genre = GenreTypes.Fantasy,
                    Rate = RateTypes.FourStars
                },
                new Movie
                {
                    Id = 5,
                    Title = "Terminator Salvation",
                    Year = new ReleaseYear(2009),
                    Genre = GenreTypes.Action,
                    Rate = RateTypes.ThreeStars
                }
            );
        }
    }
}
