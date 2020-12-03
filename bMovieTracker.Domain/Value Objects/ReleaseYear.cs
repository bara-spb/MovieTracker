using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bMovieTracker.Domain
{
    [TypeConverter(typeof(ReleaseYearConverter))]
    public class ReleaseYear : IValidatableObject
    {
        public const int MinValue = 1900;
        public const int MaxValue = 2050;

        public int? Value { get; }

        public ReleaseYear() { }

        public ReleaseYear(string year)
        {
            if (year != null && int.TryParse(year, out int yearInt))
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

        public override string ToString() => Value?.ToString();

    }
}
