namespace FengShuiNumber.Services.Interfaces
{
    public interface IFengShuiFilter
    {
        IEnumerable<string> Filter(IEnumerable<string> numbers);
        FengShuiFilter SetNumberLengthLimit(int length);
        FengShuiFilter SetFengShuiHeaderNumbers(IEnumerable<string> number);
        FengShuiFilter SetFengShuiRates(IEnumerable<decimal> rates);

        FengShuiFilter SetFengShuiNiceLastPairs(IEnumerable<string> number);
        FengShuiFilter SetFengShuiTabooLastPairs(IEnumerable<string> number);
    }
}
