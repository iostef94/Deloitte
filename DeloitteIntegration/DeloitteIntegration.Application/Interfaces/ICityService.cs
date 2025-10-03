using DeloitteIntegration.Application.DTOs;

namespace DeloitteIntegration.Application.Interfaces
{
    public interface ICityService
    {
        Task<CitySearchResultDto?> AddCityAsync(CityCreateDto dto);
        Task UpdateCityAsync(CityUpdateDto dto);
        Task DeleteCityAsync(int id);
        Task<IEnumerable<CitySearchResultDto>> SearchCityAsync(string name);
    }
}
