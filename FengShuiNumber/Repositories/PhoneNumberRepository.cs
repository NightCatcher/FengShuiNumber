using FengShuiNumber.Constants;
using FengShuiNumber.Data;
using FengShuiNumber.Data.Entities;
using FengShuiNumber.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FengShuiNumber.Repositories
{
    public class PhoneNumberRepository : IPhoneNumberRepository
    {
        private readonly ApplicationDbContext _context;
        public PhoneNumberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task AddManyAsync(IEnumerable<PhoneNumber> phoneNumbers)
        {
            _context.AddRange(phoneNumbers);
            return _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PhoneNumber>> GetAllAsync(int? pageSize = null, int pageIndex = 0)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PhoneNumber>> GetByCarrierAsync(NetworkCarrier networkCarrier, int? pageSize = null, int pageIndex = 0)
        {
            var result = _context.PhoneNumbers.Where(x=>x.NetworkCarrier == networkCarrier);
            if (pageSize != null)
            {
                result.Skip(pageIndex * pageIndex).Take(pageSize.Value);
            }

            return await result.ToListAsync();
        }
    }
}
