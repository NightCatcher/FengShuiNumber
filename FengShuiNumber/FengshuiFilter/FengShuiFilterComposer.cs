using FengShuiNumber.Dtos;
using FengShuiNumber.FengshuiFilter.Interfaces;
using FengShuiNumber.Services.Interfaces;

namespace FengShuiNumber.FengshuiFilter
{
    public class FengShuiFilterComposer : IFengShuiFilterComposer
    {
        private List<IFengShuiFilter> _filters;
        public FengShuiFilterComposer(IEnumerable<IFengShuiFilter> fengShuiFilters)
        {
            _filters = fengShuiFilters.ToList();
        }

        public FengShuiFilterComposer()
        {

        }

        public IFengShuiFilterComposer AddFilter(IFengShuiFilter filter)
        {
            if (_filters == null)
                _filters = new List<IFengShuiFilter>();
            _filters.Add(filter);
            return this;
        }

        public IEnumerable<string> Filter(FilterInput input)
        {
            if(!_filters.Any())
            {
                Console.WriteLine("There's no filter is set");
            }

            _filters = _filters.OrderBy(x => x.ConditionPriority).ToList();
            foreach (var filter in _filters)
            {
                input.Numbers = filter.DoFilter(input);
            }

            return input.Numbers;
        }
    }
}
