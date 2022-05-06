using FengShuiNumber.Dtos;
using FengShuiNumber.FengshuiFilter.Interfaces;
using FengShuiNumber.Repositories.Interfaces;
using FengShuiNumber.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace FengShuiNumber.Services
{
    public class FengShuiNumberService : IFengShuiNumberService
    {
        private readonly IPhoneNumberRepository _numberRepository;
        private readonly IFengShuiFilterComposer _fengShuiFilterComposer;
        private readonly FengShuiNumberConfiguration _settings;
        private readonly int batchSize = 500;
        public FengShuiNumberService( IOptionsSnapshot<FengShuiNumberConfiguration> optionsSnapshot,
                                    IPhoneNumberRepository numberRepository,
                                    IFengShuiFilterComposer fengShuiFilterComposer)
        {
            _numberRepository = numberRepository;
            _fengShuiFilterComposer = fengShuiFilterComposer;
            _settings = optionsSnapshot.Value;
        }

        public async Task<IEnumerable<string>> GetFengShuiNumberAsync()
        {
            var result = new List<string>();
            
            foreach (var item in _settings.HeadNumbers)
            {
                result.AddRange(await GetFengShuiNumberByCarrierAsync(item.Key));
            }

            return result;
        }

        private async Task<IEnumerable<string>> GetFengShuiNumberByCarrierAsync(string networkCarrier)
        {
            var index = 0;
            var inprocessCount = 0;
            var result = new List<string>();
            do
            {
                var numberData = await _numberRepository.GetByCarrierAsync(networkCarrier, batchSize, index);
                inprocessCount = numberData.Count();
                var numbers = numberData.Select(x => x.Number);

                var fengshuiNumbers = Filter(numbers, networkCarrier);
                result.AddRange(fengshuiNumbers);

                index++;
            } while (inprocessCount == batchSize);

            return result;
        }

        private IEnumerable<string> Filter(IEnumerable<string> numbers, string networkCarrier)
        {
            numbers = _fengShuiFilterComposer.Filter(new FilterInput
            {
                Numbers = numbers,
                NetworkCarrier = networkCarrier
            });

            return numbers;
        }
    }
}
