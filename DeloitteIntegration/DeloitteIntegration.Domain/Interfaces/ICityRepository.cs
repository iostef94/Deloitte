using DeloitteIntegration.Domain.Entities;

namespace DeloitteIntegration.Domain.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetAllAsync();
        Task<City?> GetByIdAsync(int id);
        Task AddAsync(City city);
        Task UpdateAsync(City city);
        Task DeleteAsync(int id);
        Task<IEnumerable<City>> SearchAsync(string name);

    }
}
