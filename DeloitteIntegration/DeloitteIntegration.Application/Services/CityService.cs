using DeloitteIntegration.Application.Interfaces;
using DeloitteIntegration.Domain.Entities;
using DeloitteIntegration.Domain.Interfaces;

namespace DeloitteIntegration.Application.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<City>> GetAllCitiesAsync()
        {
            return await _cityRepository.GetAllAsync();
        }

        public async Task<City?> GetCityByIdAsync(int id)
        {
            return await _cityRepository.GetByIdAsync(id);
        }

        public async Task AddCityAsync(City city)
        {
            await _cityRepository.AddAsync(city);
        }

        public async Task UpdateCityAsync(City city)
        {
            await _cityRepository.UpdateAsync(city);
        }

        public async Task DeleteCityAsync(int id)
        {
            await _cityRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<City>> SearchCityAsync(string name)
        {
            return await _cityRepository.SearchAsync(name);
        }

    }
}
