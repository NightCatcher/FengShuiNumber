using FengShuiNumber.Services.Interfaces;

namespace FengShuiNumber.Services
{
    public class FengShuiFilter : IFengShuiFilter
    {
        private IEnumerable<string> _fengshuiHeaderNumbers;
        private IEnumerable<decimal> _fengshuiRates;
        private IEnumerable<string> _niceLastPairs;
        private IEnumerable<string> _tabooLastPairs;
        private int? _numberLengthLimit;

        public FengShuiFilter()
        {

        }

        public FengShuiFilter SetNumberLengthLimit(int length)
        {
            _numberLengthLimit = length;
            return this;
        }

        public FengShuiFilter SetFengShuiHeaderNumbers(IEnumerable<string> number)
        {
            _fengshuiHeaderNumbers = number;
            return this;
        }
        public FengShuiFilter SetFengShuiRates(IEnumerable<decimal> rates)
        {
            _fengshuiRates = rates;
            return this;
        }
        public FengShuiFilter SetFengShuiNiceLastPairs(IEnumerable<string> number)
        {
            _niceLastPairs = number;
            return this;
        }
        public FengShuiFilter SetFengShuiTabooLastPairs(IEnumerable<string> number)
        {
            _tabooLastPairs = number;
            return this;
        }

        public IEnumerable<string> Filter(IEnumerable<string> numbers)
        {
            if (_numberLengthLimit == null)
                throw new ArgumentNullException("number length limit is not set");
            numbers = numbers.Where(x => x.Length == _numberLengthLimit);

            numbers = FilterFengshuiHeader(numbers);
            numbers = FilterFengshuiTabooLastPair(numbers);
            numbers = FilterFengshuiNiceLastPair(numbers);
            numbers = FilterFengshuiRate(numbers);

            return numbers;
        }

        private IEnumerable<string> FilterFengshuiHeader(IEnumerable<string> numbers)
        {
            if (_fengshuiHeaderNumbers == null)
                throw new ArgumentNullException("feng shui headers are not set");
            return numbers = numbers.Where(x => _fengshuiHeaderNumbers.Any(y => x.StartsWith(y)));
        }

        private IEnumerable<string> FilterFengshuiNiceLastPair(IEnumerable<string> numbers)
        {
            if (_fengshuiHeaderNumbers == null)
                throw new ArgumentNullException("feng shui nice last pairs are not set");
            return numbers = numbers.Where(x => _niceLastPairs.Any(y => x.EndsWith(y)));
        }

        private IEnumerable<string> FilterFengshuiTabooLastPair(IEnumerable<string> numbers)
        {
            if (_fengshuiHeaderNumbers == null)
                throw new ArgumentNullException("feng shui taboo last pairs are not set");
            return numbers = numbers.Where(x => !_tabooLastPairs.Any(y => x.EndsWith(y)));
        }

        private IEnumerable<string> FilterFengshuiRate(IEnumerable<string> numbers)
        {
            if (_fengshuiHeaderNumbers == null)
                throw new ArgumentNullException("feng shui rates are not set");

            return numbers = numbers.Where(x => HasFengshuiRate(x));
        }

        private bool HasFengshuiRate(string number)
        {
            var digits = number.Select(x => (int)char.GetNumericValue(x)).ToArray();
            var firstHalf = digits.Take(digits.Length/2).Sum();
            var secondHalf = digits.TakeLast(digits.Length/2).Sum();

            return _fengshuiRates.Any(x => x == (decimal)firstHalf/secondHalf);
        }
    }
}
