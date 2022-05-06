using FengShuiNumber.Data.Entities;

namespace FengShuiNumber.Repositories.Interfaces
{
    public interface IPhoneNumberRepository
    {
        Task<IEnumerable<PhoneNumber>> GetByCarrierAsync(string networkCarrier, int? pageSize = null, int pageIndex = 0);
        Task AddManyAsync(IEnumerable<PhoneNumber> phoneNumbers);
    }
}
