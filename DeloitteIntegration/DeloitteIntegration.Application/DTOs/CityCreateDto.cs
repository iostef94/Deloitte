namespace DeloitteIntegration.Application.DTOs
{
    public class CityCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int TouristRating { get; set; }
        public DateTime DateEstablished { get; set; }
        public int EstimatedPopulation { get; set; }
    }
}
