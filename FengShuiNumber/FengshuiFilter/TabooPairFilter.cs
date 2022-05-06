using FengShuiNumber.Dtos;
using FengShuiNumber.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace FengShuiNumber.Services
{
    public class TabooPairFilter : IFengShuiFilter
    {
        private int _priority;
        private IEnumerable<string> _tabooLastPairs;
        private readonly FengShuiNumberConfiguration _settings;

        public TabooPairFilter(IOptionsSnapshot<FengShuiNumberConfiguration> optionsSnapshot)
        {
            _settings = optionsSnapshot.Value;
            Setup();
        }
        private void Setup()
        {
            _tabooLastPairs = _settings.TabooPairNumbers;
        }

        public int ConditionPriority
        {
            get => _priority;
            set => _priority = value;
        }

        public IEnumerable<string> DoFilter(FilterInput input)
        {
            if (_tabooLastPairs == null)
                throw new ArgumentNullException("feng shui taboo last pairs are not set");
            return input.Numbers.Where(x => !_tabooLastPairs.Any(y => x.EndsWith(y)));
        }
    }
}
