namespace FengShuiNumber.Services.Interfaces
{
    public interface IFengShuiNumberService
    {
        Task<IEnumerable<string>> GetFengShuiNumberAsync();
    }
}
