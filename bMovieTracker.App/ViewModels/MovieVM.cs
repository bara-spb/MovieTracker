using bMovieTracker.App.Infrastructure;
using bMovieTracker.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace bMovieTracker.App
{
    public class MovieVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [JsonConverter(typeof(ReleaseYearJsonConverter))]
        public ReleaseYear Year { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public GenreTypes? Genre { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public RateTypes? Rate { get; set; }
    }
}
