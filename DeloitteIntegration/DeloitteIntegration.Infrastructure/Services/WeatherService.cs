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
                var data = await _httpClient.GetFromJsonAsync<WeatherApiResponse>(url);
                if (data == null) return null;

                return new WeatherInfo
                {
                    Temperature = data.Main.Temp,
                    Description = data.Weather.FirstOrDefault()?.Description ?? "Unknown"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching weather for {cityName}: {ex.Message}");
                return null;
            }
        }



    }
}
