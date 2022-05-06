using FengShuiNumber.Dtos;
using FengShuiNumber.Services.Interfaces;

namespace FengShuiNumber.Services
{
    public class HeaderValidator : IFengShuiValidator
    {
        private int _priority;
        private IEnumerable<string> _headers;

        public HeaderValidator()
        {

        }
        public void SetCondition(ConditionInput condition)
        {
            _headers = condition.Condition as IEnumerable<string>;
        }

        public int ConditionPriority
        {
            get => _priority;
            set => _priority = value;
        }

        public IEnumerable<string> Validate(IEnumerable<string> numbers)
        {
            if (_headers == null)
                throw new ArgumentNullException("feng shui headers are not set");
            return numbers = numbers.Where(x => _headers.Any(y => x.StartsWith(y)));
        }
    }
}
