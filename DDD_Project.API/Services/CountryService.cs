using DDD_Project.Domain.Entities;
using DDD_Project.Domain.Interfaces.Repositories;
using DDD_Project.Domain.Interfaces.Services;

namespace DDD_Project.API.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _repository;

        public CountryService(ICountryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            return await _repository.GetAllCountriesAsync();
        }

        public async Task<Country> GetCountryByIdAsync(int id)
        {
            return await _repository.GetCountryByIdAsync(id);
        }

        public async Task<Country> AddCountryAsync(Country country)
        {
            return await _repository.AddCountryAsync(country);
        }

        public async Task<Country> UpdateCountryAsync(Country country)
        {
            return await _repository.UpdateCountryAsync(country);
        }

        public async Task DeleteCountryAsync(int id)
        {
            await _repository.DeleteCountryAsync(id);
        }
    }
}