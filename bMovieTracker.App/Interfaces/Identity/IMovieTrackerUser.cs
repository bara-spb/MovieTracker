﻿using System;
using System.Collections.Generic;
using System.Text;

namespace bMovieTracker.App
{
    public interface IMovieTrackerUser
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
    }
}
