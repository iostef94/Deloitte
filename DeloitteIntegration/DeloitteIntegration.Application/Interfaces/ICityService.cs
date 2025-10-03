using DeloitteIntegration.Domain.Entities;

namespace DeloitteIntegration.Application.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAllCitiesAsync();
        Task<City?> GetCityByIdAsync(int id);
        Task AddCityAsync(City city);
        Task UpdateCityAsync(City city);
        Task DeleteCityAsync(int id);
        Task<IEnumerable<City>> SearchCityAsync(string name); // ✅ nou
    }
}
