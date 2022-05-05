using FengShuiNumber.Constants;
using FengShuiNumber.Repositories.Interfaces;
using FengShuiNumber.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace FengShuiNumber.Services
{
    public class FengShuiNumberService : IFengShuiNumberService
    {
        private readonly IPhoneNumberRepository _numberRepository;
        private readonly IFengShuiFilter _fengShuiFilter;
        private readonly FengShuiNumberConfiguration _settings;
        private readonly int batchSize = 500;
        public FengShuiNumberService( IOptionsSnapshot<FengShuiNumberConfiguration> optionsSnapshot,
                                    IPhoneNumberRepository numberRepository,
                                    IFengShuiFilter fengShuiFilter)
        {
            _numberRepository = numberRepository;
            _fengShuiFilter = fengShuiFilter;
            _settings = optionsSnapshot.Value;
        }

        public async Task<IEnumerable<string>> GetFengShuiNumberAsync()
        {
            var result = new List<string>();
            var networkCarrier = Enum.GetNames(typeof(NetworkCarrier));
            _fengShuiFilter.SetNumberLengthLimit(_settings.NumberLengthLimit)
                            .SetFengShuiNiceLastPairs(_settings.NicePairNumbers)
                            .SetFengShuiTabooLastPairs(_settings.TabooPairNumbers)
                            .SetFengShuiRates(ConvertFengShuiRate(_settings.FengShuiRate));

            foreach (NetworkCarrier carrier in (NetworkCarrier[])Enum.GetValues(typeof(NetworkCarrier)))
            {
                switch (carrier)
                {
                    case NetworkCarrier.Viettel:
                        _fengShuiFilter.SetFengShuiHeaderNumbers(_settings.HeadNumbers.Viettel);
                        break;
                    case NetworkCarrier.Mobiphone:
                        _fengShuiFilter.SetFengShuiHeaderNumbers(_settings.HeadNumbers.Mobifone);
                        break;
                    case NetworkCarrier.Vinaphone:
                        _fengShuiFilter.SetFengShuiHeaderNumbers(_settings.HeadNumbers.Vinaphone);
                        break;
                    default:
                        break;
                }
                result.AddRange(await GetFengShuiNumberByCarrierAsync(carrier));
            }

            return result;
        }

        private async Task<IEnumerable<string>> GetFengShuiNumberByCarrierAsync(NetworkCarrier networkCarrier)
        {
            var index = 0;
            var inprocessCount = 0;
            var result = new List<string>();
            do
            {
                var numbers = await _numberRepository.GetByCarrierAsync(networkCarrier);
                inprocessCount = numbers.Count();

                index++;
            } while (inprocessCount == batchSize);

            return result;
        }

        private IEnumerable<decimal> ConvertFengShuiRate(IEnumerable<string> rates)
        {
            foreach (var rate in rates)
            {
                var parts = rate.Split("/").Select(x => decimal.Parse(x)).ToArray();
                yield return parts[0] / parts[1];
            }
        }
    }
}
