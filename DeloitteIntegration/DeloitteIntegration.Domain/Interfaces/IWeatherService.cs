using DeloitteIntegration.Domain.DTOs;

namespace DeloitteIntegration.Domain.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherInfo?> GetWeatherAsync(string cityName);
    }
}
