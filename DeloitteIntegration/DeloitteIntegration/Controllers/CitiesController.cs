using DeloitteIntegration.Application.DTOs;
using DeloitteIntegration.Application.Interfaces;
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _cityService.AddCityAsync(dto);
            return CreatedAtAction(nameof(SearchCity), new { name = dto.Name }, result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(int id, [FromBody] CityUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest("City ID mismatch.");

            await _cityService.UpdateCityAsync(dto);
            return NoContent();
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
