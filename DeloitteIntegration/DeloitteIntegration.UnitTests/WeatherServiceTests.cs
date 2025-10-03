using DeloitteIntegration.Domain.DTOs;
using DeloitteIntegration.Infrastructure.Services;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace DeloitteIntegration.UnitTests
{
    public class WeatherServiceTests
    {
        [Fact]
        public async Task GetWeatherAsync_ShouldReturnWeatherInfo_WhenApiResponseIsValid()
        {
            // Arrange
            var jsonResponse = @"{
                ""main"": { ""temp"": 20.5 },
                ""weather"": [ { ""description"": ""clear sky"" } ]
            }";

            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonResponse)
                });

            var httpClient = new HttpClient(handlerMock.Object);
            var service = new WeatherService(httpClient, "fake_api_key");

            // Act
            var result = await service.GetWeatherAsync("Bucharest");

            // Assert
            result.Should().NotBeNull();
            result.Temperature.Should().Be(20.5);
            result.Description.Should().Be("clear sky");
        }

        [Fact]
        public async Task GetWeatherAsync_ShouldReturnNull_OnHttpError()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException());

            var httpClient = new HttpClient(handlerMock.Object);
            var service = new WeatherService(httpClient, "fake_api_key");

            // Act
            var result = await service.GetWeatherAsync("InvalidCity");

            // Assert
            result.Should().BeNull();
        }
    }
}
