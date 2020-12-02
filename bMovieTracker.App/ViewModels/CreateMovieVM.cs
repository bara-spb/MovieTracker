using bMovieTracker.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace bMovieTracker.App
{
    public class CreateMovieVM
    {
        public string Title { get; set; }
        public ReleaseYear Year { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public GenreTypes? Genre { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public RateTypes? Rate { get; set; }
    }
}
