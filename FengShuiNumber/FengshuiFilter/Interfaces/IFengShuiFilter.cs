using FengShuiNumber.Dtos;

namespace FengShuiNumber.Services.Interfaces
{
    public interface IFengShuiFilter
    {
        public int ConditionPriority { get; set; }
        IEnumerable<string> DoFilter(FilterInput input);
    }
}
