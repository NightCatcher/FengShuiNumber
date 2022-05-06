using FengShuiNumber.Dtos;
using FengShuiNumber.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace FengShuiNumber.Services
{
    public class FengshuiLengthFilter : IFengShuiFilter
    {
        private int _priority;
        private readonly FengShuiNumberConfiguration _settings;
        public FengshuiLengthFilter(IOptionsSnapshot<FengShuiNumberConfiguration> optionsSnapshot)
        {
            _settings = optionsSnapshot.Value;
        }

        public int ConditionPriority
        {
            get => _priority;
            set => _priority = value;
        }

        public IEnumerable<string> DoFilter(FilterInput input)
        {
            if (_settings.NumberLengthLimit == 0)
                throw new ArgumentNullException("feng shui rates are not set");

            var numbers = input.Numbers.Where(x => x.Length == _settings.NumberLengthLimit);

            return numbers;
        }
    }
}
