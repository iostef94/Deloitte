using DeloitteIntegration.Domain.DTOs;
using DeloitteIntegration.Domain.Interfaces;
using System.Net.Http.Json;

namespace DeloitteIntegration.Infrastructure.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherService(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        }

        public async Task<WeatherInfo?> GetWeatherAsync(string cityName)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={_apiKey}&units=metric";

            try
            {
                var data = await _httpClient.GetFromJsonAsync<dynamic>(url);
                if (data == null) return null;

                return new WeatherInfo
                {
                    Temperature = (double)data.main.temp,
                    Description = data.weather[0].description
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
