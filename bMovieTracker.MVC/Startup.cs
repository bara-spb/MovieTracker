using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using bMovieTracker.Data.Configuration;
using bMovieTracker.App.Configuration;
using AutoMapper;
using bMovieTracker.App;
using bMovieTracker.Api.Infrastructure;
using bMovieTracker.Identity.Configuration;

namespace bMovieTracker.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.ConfigureMovieTrackerIdentity(Configuration.GetConnectionString("usersDB"));
            services.ConfigureMovieTrackerData(Configuration.GetConnectionString("moviesDB"));
            services.AddAutoMapper(typeof(MovieMappingProfile));
            services.AddTransient<IMovieTrackerService, MovieTrackerService>();

            services.AddMvc(
                options => options.ModelBinderProviders.Insert(0, new MovieModelBinderProvider())                
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
