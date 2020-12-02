using bMovieTracker.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace bMovieTracker.Data.Configurations
{
    class MovieConfiguration
    {
        public MovieConfiguration(EntityTypeBuilder<Movie> entity)
        {
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Title)
                .IsRequired();
            entity.Property(e => e.Year)
                .HasConversion(
                    v => v.Value,
                    v => new ReleaseYear(v));
            entity.Property(e => e.Genre)
                .HasConversion(
                    v => v.ToString(),
                    v => v != null ? (GenreTypes)Enum.Parse(typeof(GenreTypes), v) : GenreTypes.Undefined);
            entity.Property(e => e.Rate)
                .HasConversion(
                    v => v.ToString(),
                    v => v != null ? (RateTypes)Enum.Parse(typeof(RateTypes), v) : RateTypes.Undefined);
        }
    }
}
