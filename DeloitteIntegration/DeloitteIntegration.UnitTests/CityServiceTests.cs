using DeloitteIntegration.Application.Services;
using DeloitteIntegration.Domain.Entities;
using DeloitteIntegration.Domain.Interfaces;
using Moq;
using Xunit;
using FluentAssertions;

namespace DeloitteIntegration.UnitTests
{
    public class CityServiceTests
    {
        private readonly Mock<ICityRepository> _mockRepo;
        private readonly CityService _service;

        public CityServiceTests()
        {
            _mockRepo = new Mock<ICityRepository>();
            _service = new CityService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllCitiesAsync_ShouldReturnListOfCities()
        {
            var cities = new List<City>
            {
                new City { Id = 1, Name = "Bucharest", Country = "Romania" },
                new City { Id = 2, Name = "Paris", Country = "France" }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(cities);

            var result = await _service.GetAllCitiesAsync();

            result.Should().HaveCount(2);
            result.First().Name.Should().Be("Bucharest");
        }

        [Fact]
        public async Task AddCityAsync_ShouldCallRepositoryAdd()
        {
            var city = new City { Name = "Berlin", Country = "Germany" };

            await _service.AddCityAsync(city);

            _mockRepo.Verify(r => r.AddAsync(city), Times.Once);
        }
    }
}
