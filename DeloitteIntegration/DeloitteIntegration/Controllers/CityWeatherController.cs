using DeloitteIntegration.Application.Interfaces;
using DeloitteIntegration.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DeloitteIntegration.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityWeatherController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IWeatherService _weatherService;

        public CityWeatherController(ICityService cityService, IWeatherService weatherService)
        {
            _cityService = cityService;
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeatherForAllCities()
        {
            var cities = await _cityService.GetAllCitiesAsync();

            if (cities == null || !cities.Any())
                return NotFound("No cities found in database.");

            var result = new List<object>();

            foreach (var city in cities)
            {
                try
                {
                    var weatherJson = await _weatherService.GetWeatherAsync(city.Name);
                    
                    result.Add(new
                    {
                        City = city.Name,
                        Country = city.Country,
                        Temperature = weatherJson?.Temperature,
                        Description = weatherJson?.Description.ToString()
                    });
                }
                catch (Exception ex)
                {
                    result.Add(new
                    {
                        City = city.Name,
                        Country = city.Country,
                        Error = $"Failed to fetch weather: {ex.Message}"
                    });
                }
            }

            return Ok(result);
        }

    }
}
