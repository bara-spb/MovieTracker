using System;

namespace bMovieTracker.Domain
{
    public class bMovieTrackerException : Exception
    {
        public bMovieTrackerException(string message) : this(message, null) { }

        public bMovieTrackerException(string message, Exception innerException) : base(message, innerException) { }
    }
}
