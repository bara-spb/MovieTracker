using System;
using bMovieTracker.Domain;

namespace bMovieTracker.Domain
{
    public class BMovieTrackerAccessDeniedException : bMovieTrackerException
    {
        public BMovieTrackerAccessDeniedException(string message) : this(message, null) { }

        public BMovieTrackerAccessDeniedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
