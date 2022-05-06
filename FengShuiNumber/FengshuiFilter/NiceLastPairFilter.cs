using FengShuiNumber.Dtos;
using FengShuiNumber.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace FengShuiNumber.Services
{
    public class NiceLastPairFilter : IFengShuiFilter
    {
        private int _priority;
        private IEnumerable<string> _niceLastPairs;
        private readonly FengShuiNumberConfiguration _settings;

        public NiceLastPairFilter(IOptionsSnapshot<FengShuiNumberConfiguration> optionsSnapshot)
        {
            _settings = optionsSnapshot.Value;
            Setup();
        }
        void Setup()
        {
            _niceLastPairs = _settings.NicePairNumbers;
        }

        public int ConditionPriority
        {
            get => _priority;
            set => _priority = value;
        }

        public IEnumerable<string> DoFilter(FilterInput input)
        {
            if (_niceLastPairs == null)
                throw new ArgumentNullException("feng shui nice last pairs are not set");
            return input.Numbers.Where(x => _niceLastPairs.Any(y => x.EndsWith(y)));
        }
    }
}
