using FengShuiNumber.Dtos;
using FengShuiNumber.Repositories.Interfaces;
using FengShuiNumber.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace FengShuiNumber.Services
{
    public class FengShuiNumberService : IFengShuiNumberService
    {
        private readonly IPhoneNumberRepository _numberRepository;
        private readonly IEnumerable<IFengShuiValidator> _fengShuiValidators;
        private readonly FengShuiNumberConfiguration _settings;
        private readonly int batchSize = 500;
        public FengShuiNumberService( IOptionsSnapshot<FengShuiNumberConfiguration> optionsSnapshot,
                                    IPhoneNumberRepository numberRepository,
                                    IEnumerable<IFengShuiValidator> fengShuiValidators)
        {
            _numberRepository = numberRepository;
            _fengShuiValidators = fengShuiValidators;
            _settings = optionsSnapshot.Value;
        }

        public async Task<IEnumerable<string>> GetFengShuiNumberAsync()
        {
            var result = new List<string>();
            
            foreach (var item in _settings.HeadNumbers)
            {
                SetupValidators(_fengShuiValidators, item.Key);
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
                var numbers = await _numberRepository.GetByCarrierAsync(networkCarrier, batchSize, index);
                inprocessCount = numbers.Count();
                var fengshuiNumbers = Validate(numbers.Select(x=>x.Number));
                result.AddRange(fengshuiNumbers);

                index++;
            } while (inprocessCount == batchSize);

            return result;
        }

        private IEnumerable<string> Validate(IEnumerable<string> numbers)
        {
            foreach (var validator in _fengShuiValidators)
            {
                numbers = validator.Validate(numbers);
                if (!numbers.Any())
                    break;
            }

            return numbers;
        }

        private IEnumerable<IFengShuiValidator> SetupValidators(IEnumerable<IFengShuiValidator> validators, string networkCarrier)
        {
            foreach (var item in _fengShuiValidators)
            {
                var validatorType = item.GetType().Name;
                switch (validatorType)
                {
                    case nameof(FengShuiRateValidator):
                        item.SetCondition(new ConditionInput
                        {
                            Condition = _settings.FengShuiRate
                        });
                        item.ConditionPriority = 1;
                        break;
                    case nameof(HeaderValidator):
                        item.SetCondition(new ConditionInput
                        {
                            Condition = _settings.HeadNumbers.FirstOrDefault(x=>x.Key.Equals(networkCarrier, StringComparison.OrdinalIgnoreCase)).Value
                        });
                        item.ConditionPriority = 1;
                        break;
                    case nameof(NiceLastPairValidator):
                        item.SetCondition(new ConditionInput
                        {
                            Condition = _settings.NicePairNumbers
                        });
                        item.ConditionPriority = 1;
                        break;
                    case nameof(TabooPairValidator):
                        item.SetCondition(new ConditionInput
                        {
                            Condition = _settings.TabooPairNumbers
                        });
                        item.ConditionPriority = 1;
                        break;
                    default:
                        break;
                }
                validators.OrderBy(x => x.ConditionPriority);
            }

            return validators;
        }
    }
}
