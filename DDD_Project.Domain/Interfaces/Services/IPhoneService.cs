using DDD_Project.Domain.Entities;

namespace DDD_Project.Domain.Interfaces.Services
{
    public interface IPhoneService
    {
        Task<List<Phone>> GetAllPhonesAsync();
        Task<Phone> GetPhoneByIdAsync(int id);
        Task<Phone> AddPhoneAsync(Phone phone);
        Task<Phone> UpdatePhoneAsync(Phone phone);
        Task DeletePhoneAsync(int id);
    }
}