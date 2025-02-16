using DDD_Project.Domain.Entities;
using DDD_Project.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DDD_Project.Infrastructure.Data.Repositories
{
    public class PhoneRepository : IPhoneRepository
    {
        private readonly ApplicationDbContext _context;

        public PhoneRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Phone>> GetAllPhonesAsync()
        {
            return await _context.Phones.Include(p => p.Country).OrderBy(p => p.Id).ToListAsync();
        }

        public async Task<Phone> GetPhoneByIdAsync(int id)
        {
            return await _context.Phones.Include(p => p.Country).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Phone> AddPhoneAsync(Phone phone)
        {
            _context.Phones.Add(phone);
            await _context.SaveChangesAsync();
            return phone;
        }

        public async Task<Phone> UpdatePhoneAsync(Phone phone)
        {
            _context.Phones.Update(phone);
            await _context.SaveChangesAsync();
            return phone;
        }

        public async Task DeletePhoneAsync(int id)
        {
            var phone = await _context.Phones.FindAsync(id);
            if (phone != null)
            {
                _context.Phones.Remove(phone);
                await _context.SaveChangesAsync();
            }
        }
    }
}