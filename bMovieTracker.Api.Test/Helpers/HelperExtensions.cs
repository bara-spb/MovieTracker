

namespace bMovieTracker.Api.Test.Helpers
{
    public static class HelperExtensions
    {
        public static object GetPropValue(this object obj, string propName)
        {
            return obj?.GetType().GetProperty(propName)?.GetValue(obj);
        }

        public static bool HasProperty(this object obj, string propName)
        {
            var propValue = obj?.GetPropValue(propName);
            return !string.IsNullOrEmpty(propValue?.ToString());
        }
    }
}
