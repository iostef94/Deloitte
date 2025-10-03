using DeloitteIntegration.Domain.DTOs;

namespace DeloitteIntegration.Domain.Interfaces
{
    public interface ICountryService
    {
        Task<CountryInfo?> GetCountryInfoAsync(string countryName);
    }
}
