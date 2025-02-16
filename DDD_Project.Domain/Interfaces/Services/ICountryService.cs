using DDD_Project.Domain.Entities;

namespace DDD_Project.Domain.Interfaces.Services
{
    public interface ICountryService
    {
        Task<List<Country>> GetAllCountriesAsync();
        Task<Country> GetCountryByIdAsync(int id);
        Task<Country> AddCountryAsync(Country country);
        Task<Country> UpdateCountryAsync(Country country);
        Task DeleteCountryAsync(int id);
    }
}