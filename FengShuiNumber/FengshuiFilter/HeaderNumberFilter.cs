using FengShuiNumber.Dtos;
using FengShuiNumber.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace FengShuiNumber.Services
{
    public class HeaderNumberFilter : IFengShuiFilter
    {
        private int _priority;
        private IDictionary<string, IEnumerable<string>> _headerNumbers;
        private readonly FengShuiNumberConfiguration _settings;

        public HeaderNumberFilter(IOptionsSnapshot<FengShuiNumberConfiguration> optionsSnapshot)
        {
            _settings = optionsSnapshot.Value;
            Setup();
        }

        void Setup()
        {
            _headerNumbers = _settings.HeadNumbers;
        }

        public int ConditionPriority
        {
            get => _priority;
            set => _priority = value;
        }

        public IEnumerable<string> DoFilter(FilterInput input)
        {
            var fengshuiHeaderNumber = _headerNumbers.FirstOrDefault(x => x.Key.Equals(input.NetworkCarrier, StringComparison.OrdinalIgnoreCase)).Value;
            if (fengshuiHeaderNumber == null)
                throw new ArgumentNullException("feng shui headers are not set");
            
            return input.Numbers.Where(x => fengshuiHeaderNumber.Any(y => x.StartsWith(y)));
        }
    }
}
