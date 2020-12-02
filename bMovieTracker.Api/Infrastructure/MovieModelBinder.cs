using bMovieTracker.App;
using bMovieTracker.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace bMovieTracker.Api.Infrastructure
{
    public class MovieModelBuilder : IModelBinder
    {
        private readonly IModelBinder fallbackBinder;
        public MovieModelBuilder(IModelBinder fallbackBinder)
        {
            this.fallbackBinder = fallbackBinder;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            string valueFromBody = string.Empty;

            using (var sr = new StreamReader(bindingContext.HttpContext.Request.Body))
            {
                valueFromBody = sr.ReadToEnd();
            }

            if (string.IsNullOrEmpty(valueFromBody))
            {
                return Task.CompletedTask;
            }
            var data = JsonConvert.DeserializeObject<MovieVM>(valueFromBody);

            //string values = Convert.ToString(((JValue)JObject.Parse(valueFromBody)["value"]).Value);

            //string values = Convert.ToString(((JValue)JObject.Parse(valueFromBody)["value"]).Value);

            //var splitData = values.Split(new char[] { '|' });
            //if (splitData.Length >= 2)
            //{
            //    var result = new User1
            //    {
            //        Id = Convert.ToInt32(splitData[0]),
            //        Name = splitData[1]
            //    };
            //    bindingContext.Result = ModelBindingResult.Success(result);
            //}

            return Task.CompletedTask;
        }
    }
}
