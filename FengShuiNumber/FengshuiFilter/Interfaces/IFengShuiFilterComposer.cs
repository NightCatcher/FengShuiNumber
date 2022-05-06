using FengShuiNumber.Dtos;
using FengShuiNumber.Services.Interfaces;

namespace FengShuiNumber.FengshuiFilter.Interfaces
{
    public interface IFengShuiFilterComposer
    {
        IFengShuiFilterComposer AddFilter(IFengShuiFilter filter);
        IEnumerable<string> Filter(FilterInput input);
    }
}
