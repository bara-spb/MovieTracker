using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace bMovieTracker.Identity.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureMovieTrackerIdentityForMvc(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<MovieTrackerIdentityDbContext>(options =>
                options.UseSqlServer(connectionString));


            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.AddDefaultIdentity<MovieTrackerUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddUserManager<MovieTrackerUserManager>()
                .AddSignInManager<MovieTrackerSignInManager>()
                .AddEntityFrameworkStores<MovieTrackerIdentityDbContext>();

            services.AddAuthentication();


            return services;
        }


        public static IServiceCollection ConfigureMovieTrackerIdentityForApi(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<MovieTrackerIdentityDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDefaultIdentity<MovieTrackerUser>()
                .AddUserManager<MovieTrackerUserManager>()
                .AddSignInManager<MovieTrackerSignInManager>()
                .AddEntityFrameworkStores<MovieTrackerIdentityDbContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = JwtAuthOptions.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = JwtAuthOptions.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = JwtAuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });

            return services;
        }
    }
}

