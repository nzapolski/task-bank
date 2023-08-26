using HeyBanking.API.Models;
using HeyBanking.App.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HeyBanking.API
{
    internal static class RegisterServices
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, DummyUserService>();

            services.AddHttpContextAccessor();

            //services.AddHealthChecks()
            //    .AddDbContextCheck<ApplicationDbContext>();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            return services;
        }
    }
}
