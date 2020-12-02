using bMovieTracker.App;
using bMovieTracker.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace bMovieTracker.Api.Infrastructure
{
    public class MovieModelBuilder : IModelBinder
    {

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var idResult = bindingContext.ValueProvider.GetValue("Id").FirstValue;
            var titleResult = bindingContext.ValueProvider.GetValue("Title").FirstValue;
            var yearResult = bindingContext.ValueProvider.GetValue("Year").FirstValue;
            var genreResult = bindingContext.ValueProvider.GetValue("Genre").FirstValue;
            var rateResult = bindingContext.ValueProvider.GetValue("Rate").FirstValue;

            int.TryParse(idResult, out int id);
            object result = null;
            if (bindingContext.ModelType == typeof(MovieVM))
            {
                var movieVM = new MovieVM()
                {
                    Id = id,
                    Title = titleResult,
                    Year = new ReleaseYear(yearResult),
                    Genre = GetGenre(genreResult),
                    Rate = GetRate(rateResult)
                };
                result = movieVM;
            }
            else if (bindingContext.ModelType == typeof(CreateMovieVM))
            {
                var movieVM = new CreateMovieVM()
                {
                    Title = titleResult,
                    Year = new ReleaseYear(yearResult),
                    Genre = GetGenre(genreResult),
                    Rate = GetRate(rateResult)
                };
                result = movieVM;
            }
            bindingContext.Result = ModelBindingResult.Success(result);

            return Task.CompletedTask;
        }

        private RateTypes? GetRate(object value)
        {
            if (value == null)
                return null;
            var rateStr = value.ToString();
            // int value
            if (int.TryParse(rateStr, out int rateInt) && Enum.IsDefined(typeof(RateTypes), rateInt))
                return (RateTypes?)rateInt;
            // string value
            else if (Enum.TryParse(rateStr, out RateTypes rate))
                return rate;
            return null;
        }

        private GenreTypes? GetGenre(object value)
        {
            if (value == null)
                return null;
            var genreStr = value.ToString();
            // int value
            if (int.TryParse(genreStr, out int genreInt) && Enum.IsDefined(typeof(GenreTypes), genreInt))
                return (GenreTypes?)genreInt;
            // string value
            else if (Enum.TryParse(genreStr, out GenreTypes genre))
                return genre;
            return null;
        }
    }
}
