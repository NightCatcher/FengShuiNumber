using FengShuiNumber.Dtos;
using FengShuiNumber.Services.Interfaces;

namespace FengShuiNumber.Services
{
    public class NiceLastPairValidator : IFengShuiValidator
    {
        private int _priority;
        private IEnumerable<string> _niceLastPairs;

        public NiceLastPairValidator()
        {

        }

        public void SetCondition(ConditionInput input)
        {
            _niceLastPairs = input.Condition as IEnumerable<string>;
        }

        public int ConditionPriority
        {
            get => _priority;
            set => _priority = value;
        }

        public IEnumerable<string> Validate(IEnumerable<string> numbers)
        {
            if (_niceLastPairs == null)
                throw new ArgumentNullException("feng shui nice last pairs are not set");
            return numbers = numbers.Where(x => _niceLastPairs.Any(y => x.EndsWith(y)));
        }
    }
}
