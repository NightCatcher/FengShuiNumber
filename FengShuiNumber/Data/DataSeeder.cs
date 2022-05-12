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
                new PhoneNumber { NetworkCarrier = "Viettel", Number = "0864813549" },
                new PhoneNumber { NetworkCarrier = "Viettel", Number = "0864613549" },
                new PhoneNumber { NetworkCarrier = "Viettel", Number = "0964813575" },
                new PhoneNumber { NetworkCarrier = "Viettel", Number = "0964813549" },
                new PhoneNumber { NetworkCarrier = "Viettel", Number = "0974813566" },
                //vina
                new PhoneNumber { NetworkCarrier = "Vinaphone", Number = "0894813549" },
                new PhoneNumber { NetworkCarrier = "Vinaphone", Number = "0894817549" },
                new PhoneNumber { NetworkCarrier = "Vinaphone", Number = "0904883549" },
                new PhoneNumber { NetworkCarrier = "Vinaphone", Number = "0934813549" },
                // mobi
                new PhoneNumber { NetworkCarrier = "Mobiphone", Number = "0889613549" },
                new PhoneNumber { NetworkCarrier = "Mobiphone", Number = "0884813549" },
                new PhoneNumber { NetworkCarrier = "Mobiphone", Number = "0914843549" },
                new PhoneNumber { NetworkCarrier = "Mobiphone", Number = "0914813549" },
                new PhoneNumber { NetworkCarrier = "Mobiphone", Number = "0916829926" },
            };

            await _numberRepository.AddManyAsync(numbers);
        }
    }

    public interface IDataSeeder
    {
        Task SeedAsync();
    }
}
