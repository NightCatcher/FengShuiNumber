using FengShuiNumber.Constants;
using FengShuiNumber.Data.Entities;

namespace FengShuiNumber.Repositories.Interfaces
{
    public interface IPhoneNumberRepository
    {
        Task<IEnumerable<PhoneNumber>> GetAllAsync(int? pageSize = null, int pageIndex = 0);
        Task<IEnumerable<PhoneNumber>> GetByCarrierAsync(NetworkCarrier networkCarrier, int? pageSize = null, int pageIndex = 0);
        Task AddManyAsync(IEnumerable<PhoneNumber> phoneNumbers);
    }
}
