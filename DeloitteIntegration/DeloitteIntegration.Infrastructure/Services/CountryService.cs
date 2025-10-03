using DeloitteIntegration.Domain.DTOs;
using DeloitteIntegration.Domain.Interfaces;
using System.Net.Http.Json;

namespace DeloitteIntegration.Infrastructure.Services
{
    public class CountryService : ICountryService
    {
        private readonly HttpClient _httpClient;

        public CountryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CountryInfo?> GetCountryInfoAsync(string countryName)
        {
            var url = $"https://restcountries.com/v3.1/name/{countryName}?fullText=true";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<dynamic>>(url);
                if (response == null || response.Count == 0) return null;

                var country = response[0];
                return new CountryInfo
                {
                    CountryName = country.name.common,
                    CountryCode2 = country.cca2,
                    CountryCode3 = country.cca3,
                    CurrencyCode = ((IDictionary<string, object>)country.currencies).Keys.First()
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
