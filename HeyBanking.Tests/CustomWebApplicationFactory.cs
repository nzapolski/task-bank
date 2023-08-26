using HeyBanking.API;
using HeyBanking.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HeyBanking.Tests
{
    internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                var integrationConfig = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .Build();

                configurationBuilder.AddConfiguration(integrationConfig);
            });

            builder.ConfigureServices((builder, services) =>
            {
                services
                    .Remove<DbContextOptions<ApplicationDbContext>>()
                    .AddDbContext<ApplicationDbContext>(options => options.UseSqlite($"Data Source={Testing.TestDbFile}",
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            });
        }
    }
}