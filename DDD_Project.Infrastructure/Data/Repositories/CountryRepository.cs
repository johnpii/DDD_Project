using DDD_Project.Domain.Entities;
using DDD_Project.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DDD_Project.Infrastructure.Data.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _context;

        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            return await _context.Countries.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<Country> GetCountryByIdAsync(int id)
        {
            return await _context.Countries.FindAsync(id);
        }

        public async Task<Country> AddCountryAsync(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            return country;
        }

        public async Task<Country> UpdateCountryAsync(Country country)
        {
            _context.Countries.Update(country);
            await _context.SaveChangesAsync();
            return country;
        }

        public async Task DeleteCountryAsync(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country != null)
            {
                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();
            }
        }
    }
}