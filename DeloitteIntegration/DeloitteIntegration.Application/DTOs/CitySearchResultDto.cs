namespace DeloitteIntegration.Application.DTOs
{
    public class CitySearchResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int TouristRating { get; set; }
        public DateTime DateEstablished { get; set; }
        public int EstimatedPopulation { get; set; }

        // From external APIs
        public string CountryCode2 { get; set; } = string.Empty;
        public string CountryCode3 { get; set; } = string.Empty;
        public string CurrencyCode { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public string WeatherDescription { get; set; } = string.Empty;
    }
}
