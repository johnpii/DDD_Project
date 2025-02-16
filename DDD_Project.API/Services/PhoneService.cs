using DDD_Project.Domain.Entities;
using DDD_Project.Domain.Interfaces.Repositories;
using DDD_Project.Domain.Interfaces.Services;

namespace DDD_Project.API.Services
{
    public class PhoneService : IPhoneService
    {
        private readonly IPhoneRepository _repository;

        public PhoneService(IPhoneRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Phone>> GetAllPhonesAsync()
        {
            return await _repository.GetAllPhonesAsync();
        }

        public async Task<Phone> GetPhoneByIdAsync(int id)
        {
            return await _repository.GetPhoneByIdAsync(id);
        }

        public async Task<Phone> AddPhoneAsync(Phone phone)
        {
            return await _repository.AddPhoneAsync(phone);
        }

        public async Task<Phone> UpdatePhoneAsync(Phone phone)
        {
            return await _repository.UpdatePhoneAsync(phone);
        }

        public async Task DeletePhoneAsync(int id)
        {
            await _repository.DeletePhoneAsync(id);
        }
    }
}