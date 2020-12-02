using bMovieTracker.App;
using bMovieTracker.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace bMovieTracker.Api.Infrastructure
{
    public class ReleaseYearModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            // Для объекта SimpleTypeModelBinder необходим сервис ILoggerFactory
            // Получаем его из сервисов
            ILoggerFactory loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();
            IModelBinder binder = new ReleaseYearModelBinder(new SimpleTypeModelBinder(typeof(ReleaseYear), loggerFactory));
            return context.Metadata.ModelType == typeof(ReleaseYear) ? binder : null;
        }
    }
}
