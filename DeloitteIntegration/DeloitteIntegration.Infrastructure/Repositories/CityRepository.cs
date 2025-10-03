using DeloitteIntegration.Domain.Entities;
using DeloitteIntegration.Domain.Interfaces;
using DeloitteIntegration.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DeloitteIntegration.Infrastructure.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDbContext _context;

        public CityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<City> AddAsync(City city)
        {
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
            return city;
        }

        public async Task DeleteAsync(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city != null)
            {
                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<City?> GetByIdAsync(int id)
        {
            return await _context.Cities.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<City>> SearchByNameAsync(string name)
        {
            return await _context.Cities
                .Where(c => c.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

        public async Task UpdateAsync(City city)
        {
            _context.Cities.Update(city);
            await _context.SaveChangesAsync();
        }
    }
}
