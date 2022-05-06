using FengShuiNumber.Dtos;
using FengShuiNumber.Services.Interfaces;

namespace FengShuiNumber.Services
{
    public class FengShuiRateValidator : IFengShuiValidator
    {
        private int _priority;
        private IEnumerable<decimal> _rates;
        public FengShuiRateValidator()
        {

        }

        public int ConditionPriority 
        { 
            get => _priority; 
            set => _priority = value; 
        }

        public void SetCondition(ConditionInput condition)
        {
            _rates = ConvertFengShuiRate(condition.Condition as IEnumerable<string>);
        }

        public IEnumerable<string> Validate(IEnumerable<string> numbers)
        {
            if (_rates == null)
                throw new ArgumentNullException("feng shui rates are not set");

            return numbers = numbers.Where(x => HasFengshuiRate(x));
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
                yield return parts[0] / parts[1];
            }
        }
    }
}
