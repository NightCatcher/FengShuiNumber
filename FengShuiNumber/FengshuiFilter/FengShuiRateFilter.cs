using FengShuiNumber.Dtos;
using FengShuiNumber.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace FengShuiNumber.Services
{
    public class FengShuiRateFilter : IFengShuiFilter
    {
        private int _priority;
        private IEnumerable<decimal> _rates;
        private readonly FengShuiNumberConfiguration _settings;
        public FengShuiRateFilter(IOptionsSnapshot<FengShuiNumberConfiguration> optionsSnapshot)
        {
            _settings = optionsSnapshot.Value;
            Setup();
        }

        public int ConditionPriority 
        { 
            get => _priority; 
            set => _priority = value; 
        }

        public IEnumerable<string> DoFilter(FilterInput input)
        {
            if (_rates == null)
                throw new ArgumentNullException("feng shui rates are not set");

            var numbers = input.Numbers.Where(x => HasFengshuiRate(x));

            return numbers;
        }

        void Setup()
        {
            _rates = ConvertFengShuiRate(_settings.FengShuiRate);
        }

        private bool HasFengshuiRate(string number)
        {
            var digits = number.Select(x => (int)char.GetNumericValue(x)).ToArray();
            var firstHalf = digits.Take(digits.Length / 2).Sum();
            var secondHalf = digits.TakeLast(digits.Length / 2).Sum();

            return _rates.Any(x => x == (decimal)firstHalf / secondHalf);
        }

        private IEnumerable<decimal> ConvertFengShuiRate(IEnumerable<string> rates)
        {
            foreach (var rate in rates)
            {
                var parts = rate.Split("/").Select(x => decimal.Parse(x)).ToArray();
                yield return (parts[0] / parts[1]);
            }
        }
    }
}
