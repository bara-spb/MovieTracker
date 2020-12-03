using bMovieTracker.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace bMovieTracker.App
{
    public class MovieVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required field")]
        public string Title { get; set; }
        public ReleaseYear Year { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public GenreTypes? Genre { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public RateTypes? Rate { get; set; }
    }
}
