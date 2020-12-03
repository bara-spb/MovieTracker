using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bMovieTracker.Domain
{
    [TypeConverter(typeof(MovieModelConverter))]
    public class ReleaseYear : IValidatableObject
    {
        public const int MinValue = 1900;
        public const int MaxValue = 2050;
        public const string ValidationMessage = "The year should be between 1900 and 2050";

        public int? Value { get; }

        public ReleaseYear() { }

        public ReleaseYear(string year)
        {
            int yearInt;
            if (year != null && int.TryParse(year, out yearInt))
            {
                Value = yearInt;
            }
        }
        public ReleaseYear(int? year)
        {
            Value = year;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (Value != null)
            {
                if (Value < MinValue)
                    errors.Add(new ValidationResult($"ReleaseYear should be greater then {MinValue}"));
                else if (Value > MaxValue)
                    errors.Add(new ValidationResult($"ReleaseYear can't be greater then {MaxValue}"));
            }
            return errors;
        }

        public override string ToString() => Value?.ToString() ?? "Unknown";

        // for model binding (uses long as well)
        public static implicit operator int?(ReleaseYear releaseYear)
        {
            return releaseYear.Value;
        }
        public static implicit operator ReleaseYear(int? year)
        {
            return new ReleaseYear(year);
        }
        public static implicit operator long? (ReleaseYear releaseYear)
        {
            return releaseYear.Value;
        }
        public static implicit operator ReleaseYear(long? year)
        {
            return new ReleaseYear((int)year);
        }
        public static implicit operator string (ReleaseYear releaseYear)
        {
            return releaseYear?.Value?.ToString();
        }
        public static implicit operator ReleaseYear(string year)
        {
            int yearInt;
            if (int.TryParse(year, out yearInt))
                return new ReleaseYear(yearInt);
            return new ReleaseYear();
        }
    }
}
