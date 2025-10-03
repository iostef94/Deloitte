using DeloitteIntegration.Application.DTOs;
using DeloitteIntegration.Application.Interfaces;
using DeloitteIntegration.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DeloitteIntegration.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }


        [HttpPost]
        public async Task<IActionResult> AddCity([FromBody] CityCreateDto dto)
        {
            var city = new City
            {
                Name = dto.Name,
                State = dto.State,
                Country = dto.Country,
                EstimatedPopulation = dto.EstimatedPopulation,
                TouristRating = dto.TouristRating,
                DateEstablished = dto.DateEstablished
            };

            await _cityService.AddCityAsync(city);
            return Ok(city);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(int id, [FromBody] CityUpdateDto dto)
        {
            var city = new City
            {
                EstimatedPopulation = dto.EstimatedPopulation,
                TouristRating = dto.TouristRating,
                DateEstablished = dto.DateEstablished
            };

            await _cityService.UpdateCityAsync(city);
            return Ok(city);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            await _cityService.DeleteCityAsync(id);
            return NoContent();
        }


        [HttpGet("search")]
        public async Task<IActionResult> SearchCity([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("City name is required.");

            var results = await _cityService.SearchCityAsync(name);
            if (!results.Any())
                return NotFound("No cities found.");

            return Ok(results);
        }
    }
}
