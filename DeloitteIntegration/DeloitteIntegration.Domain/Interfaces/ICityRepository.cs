using DeloitteIntegration.Domain.Entities;

namespace DeloitteIntegration.Domain.Interfaces
{
    public interface ICityRepository
    {
        Task<City?> GetByIdAsync(int id);
        Task<IEnumerable<City>> SearchByNameAsync(string name);
        Task<City> AddAsync(City city);
        Task UpdateAsync(City city);
        Task DeleteAsync(int id);
    }
}
