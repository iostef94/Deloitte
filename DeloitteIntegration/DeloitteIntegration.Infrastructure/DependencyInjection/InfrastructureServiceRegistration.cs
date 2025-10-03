using DeloitteIntegration.Domain.Interfaces;
using DeloitteIntegration.Infrastructure.Data;
using DeloitteIntegration.Infrastructure.Repositories;
using DeloitteIntegration.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeloitteIntegration.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<ICityRepository, CityRepository>();

            services.AddHttpClient<ICountryService, CountryService>();

            services.AddHttpClient<IWeatherService, WeatherService>()
                .AddTypedClient((httpClient, sp) =>
                {
                    var configuration = sp.GetRequiredService<IConfiguration>();
                    var apiKey = configuration["OpenWeatherMap:ApiKey"];
                    return new WeatherService(httpClient, apiKey);
                });


            return services;
        }
    }
}
