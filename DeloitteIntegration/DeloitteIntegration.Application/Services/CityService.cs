using DeloitteIntegration.Application.DTOs;
using DeloitteIntegration.Application.Interfaces;
using DeloitteIntegration.Domain.Entities;
using DeloitteIntegration.Domain.Interfaces;

namespace DeloitteIntegration.Application.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly ICountryService _countryService;
        private readonly IWeatherService _weatherService;

        public CityService(
            ICityRepository cityRepository,
            ICountryService countryService,
            IWeatherService weatherService)
        {
            _cityRepository = cityRepository;
            _countryService = countryService;
            _weatherService = weatherService;
        }

        public async Task<CitySearchResultDto?> AddCityAsync(CityCreateDto dto)
        {
            var city = new City
            {
                Name = dto.Name,
                State = dto.State,
                Country = dto.Country,
                TouristRating = dto.TouristRating,
                DateEstablished = dto.DateEstablished,
                EstimatedPopulation = dto.EstimatedPopulation
            };

            var result = await _cityRepository.AddAsync(city);
            var countryInfo = await _countryService.GetCountryInfoAsync(result.Country);
            var weatherInfo = await _weatherService.GetWeatherAsync(result.Name);

            return new CitySearchResultDto
            {
                Id = result.Id,
                Name = result.Name,
                State = result.State,
                Country = result.Country,
                TouristRating = result.TouristRating,
                DateEstablished = result.DateEstablished,
                EstimatedPopulation = result.EstimatedPopulation,
                CountryCode2 = countryInfo?.CountryCode2 ?? string.Empty,
                CountryCode3 = countryInfo?.CountryCode3 ?? string.Empty,
                CurrencyCode = countryInfo?.CurrencyCode ?? string.Empty,
                Temperature = weatherInfo?.Temperature ?? 0,
                WeatherDescription = weatherInfo?.Description ?? string.Empty
            };
        }

        public async Task UpdateCityAsync(CityUpdateDto dto)
        {
            var city = await _cityRepository.GetByIdAsync(dto.Id);
            if (city == null) throw new KeyNotFoundException("City not found.");

            city.TouristRating = dto.TouristRating;
            city.DateEstablished = dto.DateEstablished;
            city.EstimatedPopulation = dto.EstimatedPopulation;

            await _cityRepository.UpdateAsync(city);
        }

        public async Task DeleteCityAsync(int id)
        {
            await _cityRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CitySearchResultDto>> SearchCityAsync(string name)
        {
            var cities = await _cityRepository.SearchByNameAsync(name);
            var results = new List<CitySearchResultDto>();

            foreach (var city in cities)
            {
                var countryInfo = await _countryService.GetCountryInfoAsync(city.Country);
                var weatherInfo = await _weatherService.GetWeatherAsync(city.Name);

                results.Add(new CitySearchResultDto
                {
                    Id = city.Id,
                    Name = city.Name,
                    State = city.State,
                    Country = city.Country,
                    TouristRating = city.TouristRating,
                    DateEstablished = city.DateEstablished,
                    EstimatedPopulation = city.EstimatedPopulation,
                    CountryCode2 = countryInfo?.CountryCode2 ?? string.Empty,
                    CountryCode3 = countryInfo?.CountryCode3 ?? string.Empty,
                    CurrencyCode = countryInfo?.CurrencyCode ?? string.Empty,
                    Temperature = weatherInfo?.Temperature ?? 0,
                    WeatherDescription = weatherInfo?.Description ?? string.Empty
                });
            }

            return results;
        }
    }
}
