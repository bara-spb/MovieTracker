﻿

namespace bMovieTracker.Domain
{
    public class Movie : BaseEntity
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public ReleaseYear Year { get; set; }
        public GenreTypes? Genre { get; set; }
        public RateTypes? Rate { get; set; }
    }
}
