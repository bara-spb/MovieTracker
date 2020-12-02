using System;

namespace bMovieTracker.Domain
{
    public class ReleaseYear
    {
        private static int MaxYearInFuture = 30;

        public int? Value { get; }

        public ReleaseYear() { }

        public ReleaseYear(string year)
        {
            int yearInt;
            if (year != null && int.TryParse(year, out yearInt))
            {
                Validate(yearInt);
                Value = yearInt;
            }
        }
        public ReleaseYear(int? year)
        {
            Validate(year);
            Value = year;
        }

        private void Validate(int? year)
        {
            if (year < 1900)
                throw new bMovieTrackerException("ReleaseYear should be grater than 1900");
            else if (year > DateTime.Now.Year + MaxYearInFuture)
                throw new bMovieTrackerException($"ReleaseYear can't exceed current date for more than {MaxYearInFuture} years");
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
