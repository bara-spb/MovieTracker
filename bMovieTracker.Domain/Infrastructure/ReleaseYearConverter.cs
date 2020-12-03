using System;
using System.ComponentModel;
using System.Globalization;

namespace bMovieTracker.Domain
{
    public class ReleaseYearConverter: TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(int) || sourceType == typeof(int?) || sourceType == typeof(string) || sourceType == typeof(object);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(int) || destinationType == typeof(int?) || destinationType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (int.TryParse(value?.ToString() ?? "", out int year))
                return new ReleaseYear(year);
            return new ReleaseYear();
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is ReleaseYear year)
            {
                if (destinationType == typeof(int))
                    return year.Value ?? 0;
                if (destinationType == typeof(string))
                    return year.Value?.ToString();
                else
                    return year.Value;
            }
            return null;
        }
    }
}
