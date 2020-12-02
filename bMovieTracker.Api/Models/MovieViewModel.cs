using bMovieTracker.Api.Infrastructure;
using bMovieTracker.Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace bMovieTracker.Api.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [BindProperty(BinderType = typeof(ReleaseYearModelBinder))]
        public ReleaseYear Year { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public GenreTypes? Genre { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public RateTypes? Rate { get; set; }
    }
}
