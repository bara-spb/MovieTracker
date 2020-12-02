using bMovieTracker.App;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;

namespace bMovieTracker.Api.Infrastructure
{
    public class MovieModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(MovieVM) || context.Metadata.ModelType == typeof(CreateMovieVM))
            {
                return new BinderTypeModelBinder(typeof(MovieModelBuilder));
            }

            return null;
        }
    }
}
