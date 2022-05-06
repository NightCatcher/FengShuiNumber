using FengShuiNumber.Dtos;
using FengShuiNumber.Services.Interfaces;

namespace FengShuiNumber.Services
{
    public class TabooPairValidator : IFengShuiValidator
    {
        private int _priority;
        private IEnumerable<string> _tabooLastPairs;
        public TabooPairValidator()
        {

        }

        public void SetCondition(ConditionInput input)
        {
            _tabooLastPairs = input.Condition as IEnumerable<string>;
        }

        public int ConditionPriority
        {
            get => _priority;
            set => _priority = value;
        }

        public IEnumerable<string> Validate(IEnumerable<string> numbers)
        {
            if (_tabooLastPairs == null)
                throw new ArgumentNullException("feng shui taboo last pairs are not set");
            return numbers = numbers.Where(x => !_tabooLastPairs.Any(y => x.EndsWith(y)));
        }
    }
}
