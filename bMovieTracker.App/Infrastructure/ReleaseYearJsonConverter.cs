using bMovieTracker.Domain;
using Newtonsoft.Json;
using System;

namespace bMovieTracker.App.Infrastructure
{
    public class ReleaseYearJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ReleaseYear);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var year = reader.Value;
            return new ReleaseYear(year?.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //int? year = (value as ReleaseYear)?.Value;
            serializer.Serialize(writer, value.ToString());

        }
    }
}
