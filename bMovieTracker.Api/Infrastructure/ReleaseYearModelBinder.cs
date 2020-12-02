using bMovieTracker.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bMovieTracker.Api.Infrastructure
{
    public class ReleaseYearModelBinder : IModelBinder
    {
        private readonly IModelBinder fallbackBinder;
        public ReleaseYearModelBinder(IModelBinder fallbackBinder)
        {
            this.fallbackBinder = fallbackBinder;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var yearValues = bindingContext.ValueProvider.GetValue("Year");
            if (yearValues == ValueProviderResult.None)
                return fallbackBinder.BindModelAsync(bindingContext);
            int year;
            if (int.TryParse(yearValues.FirstValue, out year))
                bindingContext.Result = ModelBindingResult.Success(new ReleaseYear(year));
            else
                bindingContext.Result = ModelBindingResult.Success(null);
            return Task.CompletedTask;
        }
    }
}
