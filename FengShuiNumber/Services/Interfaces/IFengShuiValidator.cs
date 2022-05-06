using FengShuiNumber.Dtos;

namespace FengShuiNumber.Services.Interfaces
{
    public interface IFengShuiValidator
    {
        public int ConditionPriority { get; set; }
        void SetCondition(ConditionInput condition);
        IEnumerable<string> Validate(IEnumerable<string> numbers);
    }
}
