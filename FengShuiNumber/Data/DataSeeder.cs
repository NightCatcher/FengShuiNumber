using FengShuiNumber.Data.Entities;
using FengShuiNumber.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FengShuiNumber.Data
{
    public class DataSeeder : IDataSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly IPhoneNumberRepository _numberRepository;
        public DataSeeder( ApplicationDbContext context,
                            IPhoneNumberRepository numberRepository)
        {
            _numberRepository = numberRepository;
            _context = context;
        }

        public async Task SeedAsync()
        {
            _context.Database.Migrate();
            if (await _context.PhoneNumbers.AnyAsync())
                return;
            var numbers = new List<PhoneNumber>()
            {
                //viettel
                new PhoneNumber { NetworkCarrier = Constants.NetworkCarrier.Viettel, Number = "0864813549" },
                new PhoneNumber { NetworkCarrier = Constants.NetworkCarrier.Viettel, Number = "0864813549" },
                new PhoneNumber { NetworkCarrier = Constants.NetworkCarrier.Viettel, Number = "0964813545" },
                new PhoneNumber { NetworkCarrier = Constants.NetworkCarrier.Viettel, Number = "0964813549" },
                new PhoneNumber { NetworkCarrier = Constants.NetworkCarrier.Viettel, Number = "0974813566" },
                //vina
                new PhoneNumber { NetworkCarrier = Constants.NetworkCarrier.Vinaphone, Number = "0894813549" },
                new PhoneNumber { NetworkCarrier = Constants.NetworkCarrier.Vinaphone, Number = "0894813549" },
                new PhoneNumber { NetworkCarrier = Constants.NetworkCarrier.Vinaphone, Number = "0904813549" },
                new PhoneNumber { NetworkCarrier = Constants.NetworkCarrier.Vinaphone, Number = "0934813549" },
                // mobi
                new PhoneNumber { NetworkCarrier = Constants.NetworkCarrier.Mobiphone, Number = "0884813549" },
                new PhoneNumber { NetworkCarrier = Constants.NetworkCarrier.Mobiphone, Number = "0884813549" },
                new PhoneNumber { NetworkCarrier = Constants.NetworkCarrier.Mobiphone, Number = "0914813549" },
                new PhoneNumber { NetworkCarrier = Constants.NetworkCarrier.Mobiphone, Number = "0914813549" },
                new PhoneNumber { NetworkCarrier = Constants.NetworkCarrier.Mobiphone, Number = "0914813582" },
            };

            _numberRepository.AddManyAsync(numbers);
        }
    }

    public interface IDataSeeder
    {
        Task SeedAsync();
    }
}
